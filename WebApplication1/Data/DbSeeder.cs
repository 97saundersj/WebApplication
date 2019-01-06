using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            CreateData(context);
            CreateUsers(userManager);

        }

        private static void CreateData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            //Create Posts and comments

            //1st post
            if (context.Posts.Count() == 0)
            {
                Post p1 = new Post()
                {
                    Title = "1st Post",
                    Content = "Hello!",
                    Published = new DateTime(2018, 12, 31, 13, 22, 00),
                    Username = "Member1@email.com"
                };
                context.Add(p1);

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "Hi",
                        Published = new DateTime(2018, 12, 31, 13, 25, 00),
                        Username = "Customer1@email.com",
                        MyPost = p1
                    });

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "Hello",
                        Published = new DateTime(2018, 12, 31, 13, 30, 00),
                        Username = "Customer2@email.com",
                        MyPost = p1
                    });

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "Hello All!",
                        Published = new DateTime(2018, 12, 31, 13, 33, 00),
                        Username = "Customer4@email.com",
                        MyPost = p1
                    });

                //2nd Post
                Post p2 = new Post()
                {
                    Title = "Happy New Year!",
                    Content = "Happy new year everyone!",
                    Published = new DateTime(2019, 1, 1, 00, 1, 00),
                    Username = "Member1@email.com"
                };
                context.Add(p2);

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "HNY!",
                        Published = new DateTime(2019, 1, 1, 00, 5, 00),
                        Username = "Customer3@email.com",
                        MyPost = p2
                    });

                //3rd Post
                Post p3 = new Post()
                {
                    Title = "How was everyone's holidays?",
                    Content = "I had a great time and i hope everyone did as well",
                    Published = new DateTime(2019, 1, 3, 15, 33, 00),
                    Username = "Member1@email.com"
                };
                context.Add(p3);

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "It was ok, only got socks for christmas...Again :(",
                        Published = new DateTime(2019, 1, 3, 17, 33, 00),
                        Username = "Customer3@email.com",
                        MyPost = p3
                    });

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "Not too bad",
                        Published = new DateTime(2019, 1, 3, 19, 33, 00),
                        Username = "Customer4@email.com",
                        MyPost = p3
                    });

                //4th Post
                Post p4 = new Post()
                {
                    Title = "Revision Advice",
                    Content = "Anyone have any revision tips they would like to share?",
                    Published = new DateTime(2019, 1, 4, 17, 33, 00),
                    Username = "Member1@email.com"
                };
                context.Add(p4);

                context.Comments.Add(
                    new Comment()
                    {
                        Content = "No",
                        Published = new DateTime(2019, 1, 3, 17, 33, 00),
                        Username = "Customer5@email.com",
                        MyPost = p4
                    });

                context.SaveChanges();
            }
        }

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

            userManager.AddClaimAsync(Member1, new Claim("CanCreatePosts", "True")).Wait();

            userManager.AddClaimAsync(Member1, new Claim("CanEditPosts", "True")).Wait();

            userManager.AddClaimAsync(Member1, new Claim("CanDeletePosts", "True")).Wait();
        }

    }

}

