namespace ProjectCSharp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUsers(context, "StanBotev", "Stanislav", "Botev" , "dark@dark.bg","123456");
                CreateUsers(context, "Ekateristotel", "Ekaterina", "Kaprieva", "eti@etika.bg", "123456");
                
                CreateRole(context, "Administrator");
                CreateRole(context, "User");

                AddUserToRole(context, "StanBotev", "Administrator");
                AddUserToRole(context,"Ekateristotel","User");

                CreatePost(context,
                    title: "Norway",
                    body: @"Norway has a total area of 385,252 square kilometres (148,747 sq mi) and a population of 5,213,985 (May 2016).[11] The country shares a long eastern border with Sweden (1,619 km or 1,006 mi long). Norway is bordered by Finland and Russia to the north-east, and the Skagerrak Strait to the south, with Denmark on the other side. Norway has an extensive coastline, facing the North Atlantic Ocean and the Barents Sea.",
                    date: new DateTime(2016, 03, 27, 17, 53, 48),
                    authorUsername: "StanBotev"
                );
                CreatePost(context,
                    title: "Game of Thrones",
                    body: @"The series is set on the fictional continents of Westeros and Essos and interweaves several plot lines with a large ensemble cast. The first narrative arc follows a dynastic conflict among competing claimants for succession to the Iron Throne of the Seven Kingdoms, with other noble families fighting for independence from it; the second covers the attempts to reclaim the throne by the exiled last scion of the realm's deposed ruling dynasty; the third chronicles the rising threat of the impending winter and the legendary creatures and fierce peoples of the North.",
                    date: new DateTime(2016, 08, 29, 15, 20, 48),
                    authorUsername: "StanBotev"
                );
                CreatePost(context,
                    title: "Amnesia",
                    body: @"Amnesia is an abnormal mental state in which memory and learning are affected out of all proportion to other cognitive functions in an otherwise alert and responsive patient.[5] There are two forms of amnesia: Anterograde amnesia and retrograde amnesia, that show hippocampal or medial temporal lobe damage. Anterograde amnesics show difficulty in the learning and retention of information encountered after brain damage. Retrograde Amnesics generally have memories spared about personal experiences or context independent semantic information.",
                    date: new DateTime(2016, 08, 28, 13, 20, 48),
                    authorUsername: "StanBotev"
                );


                context.SaveChanges();
            }
        }

        private void CreateUsers(ApplicationDbContext context,
            string username , string firstname , string lastname , string email, string password)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = username,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
           
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string body, DateTime date, string authorUsername)
        {
            var post = new Post();
            post.Title = title;
            post.Body = body;
            post.Date = date;
            post.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Posts.Add(post);
        }
    }
}
