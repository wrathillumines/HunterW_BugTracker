namespace HunterW_BugTracker.Migrations
{
    using HunterW_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HunterW_BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HunterW_BugTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region roleManager
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }

            #endregion

            //Create users that occupy roles
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "hwphotog@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "hwphotog@gmail.com",
                    Email = "hwphotog@gmail.com",
                    FirstName = "Hunter",
                    LastName = "Williams",
                    DisplayName = "dhunterw"
                }, "atomicSkier_92");
            }

            if (!context.Users.Any(u => u.Email == "dhwprojman@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "dhwprojman@mailinator.com",
                    Email = "dhwprojman@mailinator.com",
                    FirstName = "Project Manager",
                    LastName = "Hunter",
                    DisplayName = "dhwprojman"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "dhwdev@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "dhwdev@mailinator.com",
                    Email = "dhwdev@mailinator.com",
                    FirstName = "Developer",
                    LastName = "Hunter",
                    DisplayName = "dhwdev"
                }, "Iwantin2!");
            }

            if (!context.Users.Any(u => u.Email == "dhwsub@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "dhwsub@mailinator.com",
                    Email = "dhwsub@mailinator.com",
                    FirstName = "Submitter",
                    LastName = "Hunter",
                    DisplayName = "dhwsub"
                }, "2Bornot2B?");
            }

            //Demo Users
            if (!context.Users.Any(u => u.Email == "theadmindemouser@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "theadmindemouser@mailinator.com",
                    Email = "theadmindemouser@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "demoadmin"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "theprojectmanagerdemouser@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "theprojectmanagerdemouser@mailinator.com",
                    Email = "theprojectmanagerdemouser@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "demopm"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "thedeveloperdemouser@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "thedeveloperdemouser@mailinator.com",
                    Email = "thedeveloperdemouser@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "demodeveloper"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "thesubmitterdemouser@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "thesubmitterdemouser@mailinator.com",
                    Email = "thesubmitterdemouser@mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "demosubmitter"
                }, "D3m07h15!");
            }

            var userId = userManager.FindByEmail("hwphotog@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("dhwprojman@mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("dhwdev@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("dhwsub@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("theadmindemouser@mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("theprojectmanagerdemouser@mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("thedeveloperdemouser@mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("thesubmitterdemouser@mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
        }
    }
}
