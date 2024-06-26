﻿using Microsoft.AspNetCore.Identity;
using RealEstateApplication.Application.Enums;
using RealEstateApplication.Identity.Entities;

namespace RealEstateApplication.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "adminuser",
                Email = "adminuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Admin",
                IdentityCard = "40665606806",
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
                    await userManager.CreateAsync(defaultUser, "123AdminC#");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }

        }
    }
}
