﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApplication.Application.Dtos.User;
using RealEstateApplication.Application.Enums;
using RealEstateApplication.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApplication.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [SwaggerTag("Sistema de Membresia")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authentication")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Login de usuario",
            Description = "Incio de sesion para los usuarios del sistema"
            )]

        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debes mandar toda la informacion");
                }
                var response = await _accountService.AuthenticateAsync(request);
                if (response.HasError)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, response.Error);
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Creacion de un usuario de tipo Administrador",
            Description = "Se envia los parametros necesarios para crear un usuario de tipo Administrador"
         )]

        public async Task<IActionResult> RegisterAdmin(RegisterRequest register)
        {
            try
            {
                register.SelectRole = (int)Roles.Admin;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Envie los datos correctamente");
                }
                var response = await _accountService.RegisterAsync(register);
                if (response.HasError)
                {
                    return BadRequest(response.Error);
                }
                return StatusCode(StatusCodes.Status201Created, "Admin creado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
