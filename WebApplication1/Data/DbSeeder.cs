using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            //context.Roles.Add(new IdentityRole("ADMIN"));
            //context.SaveChanges();
            /*
            if (!roleManager.RoleExistsAsync("CanCreatePosts").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "CanCreatePosts";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            */
            

            CreateUsers(userManager);

        }

        /*private static void createData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
   
         }*/

        private static void CreateUsers(UserManager<IdentityUser> userManager)
        {
            IdentityUser Member1 = new IdentityUser
            {
                UserName = "Member1@email.com",
                Email = "Member1@email.com",
            };

            IdentityUser Customer1 = new IdentityUser
            {
                UserName = "Customer1@email.com",
                Email = "Customer1@email.com",
            };

            IdentityUser Customer2 = new IdentityUser
            {
                UserName = "Customer2@email.com",
                Email = "Customer2@email.com",
            };

            IdentityUser Customer3 = new IdentityUser
            {
                UserName = "Customer3@email.com",
                Email = "Customer3@email.com",
            };

            IdentityUser Customer4 = new IdentityUser
            {
                UserName = "Customer4@email.com",
                Email = "Customer4@email.com",
            };

            IdentityUser Customer5 = new IdentityUser
            {
                UserName = "Customer5@email.com",
                Email = "Customer5@email.com",
            };

            String password = "Password123!";

            userManager.CreateAsync(Member1, password).Wait();
            userManager.CreateAsync(Customer1, password).Wait();
            userManager.CreateAsync(Customer2, password).Wait();
            userManager.CreateAsync(Customer3, password).Wait();
            userManager.CreateAsync(Customer4, password).Wait();
            userManager.CreateAsync(Customer5, password).Wait();

            //userManager.AddClaimAsync(Member1, new Claim("CreatePosts","True")).Wait();
           // userManager.AddToRoleAsync(Member1, "CanCreatePosts").Wait();
            userManager.AddClaimAsync(Member1, new Claim("CanCreatePosts", "True")).Wait();
            userManager.AddClaimAsync(Customer1, new Claim("CanCreatePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer2, new Claim("CanCreatePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer3, new Claim("CanCreatePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer4, new Claim("CanCreatePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer5, new Claim("CanCreatePosts", "False")).Wait();

            userManager.AddClaimAsync(Member1, new Claim("CanEditPosts", "True")).Wait();
            userManager.AddClaimAsync(Customer1, new Claim("CanEditPosts", "False")).Wait();
            userManager.AddClaimAsync(Customer2, new Claim("CanEditPosts", "False")).Wait();
            userManager.AddClaimAsync(Customer3, new Claim("CanEditPosts", "False")).Wait();
            userManager.AddClaimAsync(Customer4, new Claim("CanEditPosts", "False")).Wait();
            userManager.AddClaimAsync(Customer5, new Claim("CanEditPosts", "False")).Wait();

            userManager.AddClaimAsync(Member1, new Claim("CanDeletePosts", "True")).Wait();
            userManager.AddClaimAsync(Customer1, new Claim("CanDeletePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer2, new Claim("CanDeletePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer3, new Claim("CanDeletePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer4, new Claim("CanDeletePosts", "False")).Wait();
            userManager.AddClaimAsync(Customer5, new Claim("CanDeletePosts", "False")).Wait();


        }

    }

}

