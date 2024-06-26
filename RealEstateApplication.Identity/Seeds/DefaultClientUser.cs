using Microsoft.AspNetCore.Identity;
using RealEstateApplication.Application.Enums;
using RealEstateApplication.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "clientuser",
                Email = "clientuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Cliente",
                IdentityCard = "40865662222",
                ImageUser = "/Images/Register/1895ba83-1a31-4fac-9871-96a47429a7b7/2484754c-db93-4c66-bb19-495d74dc8254.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                CountOfRealEstate = 0
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123ClientC#");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
                }
            }

        }
    }
}
