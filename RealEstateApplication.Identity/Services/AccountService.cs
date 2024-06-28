using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateApplication.Application.Dtos.User;
using RealEstateApplication.Application.Enums;
using RealEstateApplication.Application.Interfaces.Services;
using RealEstateApplication.Domain.Settings;
using RealEstateApplication.Identity.Context;
using RealEstateApplication.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealEstateApplication.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IdentityContext _identityContext;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IdentityContext identityContext, IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {

                HasError = false
            };

            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario '{request.UserName}' ya esta en uso";
                return response;
            }


            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo '{request.Email}' ya esta en uso";
                return response;
            }

            //Manejo de variables
            Dictionary<int, (bool emailConfirmed, bool isActive)> roleProperties = new()
            {
                { (int)Roles.Client, (false, false) },
                { (int)Roles.Agent, (true, false) },
                { (int)Roles.Admin, (true, true) }
            };

            (bool emailConfirmed, bool isActive) = roleProperties.TryGetValue(request.SelectRole, out (bool emailConfirmed, bool isActive) value) ? value : (false, false);

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                IdentityCard = request.IdentityCard,
                ImageUser = request.ImageUser,
                IsActive = isActive,
                EmailConfirmed = emailConfirmed
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            response.IdUser = user.Id;
            if (result.Succeeded)
            {

                switch (request.SelectRole)
                {
                    case (int)Roles.Client:
                        await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                        break;
                    case (int)Roles.Agent:
                        await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());
                        break;
                    case (int)Roles.Admin:
                        await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                        break;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Error al registrar al usuario";
                return response;
            }

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con '{request.UserName}'";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales incorrectas para '{request.UserName}'";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"El usuario '{request.UserName}' con el correo '{user.Email}' no se encuntra confirmado";
                return response;
            }

            var listRole = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            if (listRole.Contains(Roles.Agent.ToString()) || listRole.Contains(Roles.Client.ToString()))
            {
                response.HasError = true;
                response.Error = "No puedes usar la WebApi, ingresa con un usuario tipo developer o admin";
                return response;
            }
            //Mapeando el Applicationuser a Authentication Response.
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.ImageUser = user.ImageUser;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IsActive = user.IsActive;
            response.Roles = listRole.ToList();
            response.IsVerified = user.EmailConfirmed;
            //Asignando el JWT.
            JwtSecurityToken jwtSecurityToken = await GetSecurityToken(user);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<JwtSecurityToken> GetSecurityToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }
    }
}
