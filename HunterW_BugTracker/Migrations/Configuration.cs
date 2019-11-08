namespace HunterW_BugTracker.Migrations
{
    using HunterW_BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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

            #region Seed Roles

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
            if (!context.Roles.Any(r => r.Name == "Demo Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Demo Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo Project Manager" });
            }
            if (!context.Roles.Any(r => r.Name == "Demo Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Demo Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Demo Submitter" });
            }

            #endregion

            #region Seed Users

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
                    DisplayName = "dhunterw",
                    AvatarUrl = "/Images/hunterw.jpg"
                }, "atomicSkier_92");
            }

            if (!context.Users.Any(u => u.Email == "nbarber@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "nbarber@hwerrortracker.com",
                    Email = "nbarber@hwerrortracker.com",
                    FirstName = "Nancy",
                    LastName = "Barber",
                    DisplayName = "nbarber",
                    AvatarUrl = "/Images/nancyb.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "bmorris@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "bmorris.com",
                    Email = "bmorris@hwerrortracker.com",
                    FirstName = "Brian",
                    LastName = "Morris",
                    DisplayName = "bmorris",
                    AvatarUrl = "/Images/brianm.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "awilson@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "awilson@hwerrortracker.com",
                    Email = "awilson@hwerrortracker.com",
                    FirstName = "Amanda",
                    LastName = "Wilson",
                    DisplayName = "awilson",
                    AvatarUrl = "/Images/amandaw.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "smartin@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "smartin@hwerrortracker.com",
                    Email = "smartin@hwerrortracker.com",
                    FirstName = "Shannon",
                    LastName = "Martin",
                    DisplayName = "smartin",
                    AvatarUrl = "/Images/shannonm.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "dbutler@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "dbutler@hwerrortracker.com",
                    Email = "dbutler@hwerrortracker.com",
                    FirstName = "Daniel",
                    LastName = "Butler",
                    DisplayName = "dbutler",
                    AvatarUrl = "/Images/danielb.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "jwhite@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "jwhite@hwerrortracker.com",
                    Email = "jwhite@hwerrortracker.com",
                    FirstName = "James",
                    LastName = "White",
                    DisplayName = "jwhite",
                    AvatarUrl = "/Images/jamesw.jpg"
                }, "letMein2!");
            }

            if (!context.Users.Any(u => u.Email == "sroberts@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "sroberts@hwerrortracker.com",
                    Email = "sroberts@hwerrortracker.com",
                    FirstName = "Stan",
                    LastName = "Roberts",
                    DisplayName = "sroberts",
                    AvatarUrl = "/Images/stanr.jpg"
                }, "letMein2!");
            }

            //Demo Users
            if (!context.Users.Any(u => u.Email == "theadmindemouser@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "theadmindemouser@hwerrortracker.com",
                    Email = "theadmindemouser@hwerrortracker.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "demoadmin",
                    AvatarUrl = "/Images/TheAdmin.png"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "theprojectmanagerdemouser@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "theprojectmanagerdemouser@hwerrortracker.com",
                    Email = "theprojectmanagerdemouser@hwerrortracker.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "demopm",
                    AvatarUrl = "/Images/TheProjectManager.png"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "thedeveloperdemouser@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "thedeveloperdemouser@hwerrortracker.com",
                    Email = "thedeveloperdemouser@hwerrortracker.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "demodeveloper",
                    AvatarUrl = "/Images/TheDeveloper.png"
                }, "D3m07h15!");
            }

            if (!context.Users.Any(u => u.Email == "thesubmitterdemouser@hwerrortracker.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "thesubmitterdemouser@hwerrortracker.com",
                    Email = "thesubmitterdemouser@hwerrortracker.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "demosubmitter",
                    AvatarUrl = "/Images/TheSubmitter.png"
                }, "D3m07h15!");
            }

            #endregion

            #region Add seeded users to roles

            var userId = userManager.FindByEmail("hwphotog@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("nbarber@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("bmorris@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("awilson@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("theadmindemouser@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Demo Admin");

            userId = userManager.FindByEmail("theprojectmanagerdemouser@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Demo Project Manager");

            userId = userManager.FindByEmail("thedeveloperdemouser@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Demo Developer");

            userId = userManager.FindByEmail("thesubmitterdemouser@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Demo Submitter");

            userId = userManager.FindByEmail("smartin@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("dbutler@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Project Manager");

            userId = userManager.FindByEmail("sroberts@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Submitter");

            userId = userManager.FindByEmail("jwhite@hwerrortracker.com").Id;
            userManager.AddToRole(userId, "Developer");

            #endregion

            context.SaveChanges();


            #region Ticket Types

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Complaint", Description = "A user has filed a complaint about the application." },
                    new TicketType { Name = "Error", Description = "A user has reported an error within the application." },
                    new TicketType { Name = "Typo", Description = "A user has reported a spelling and/or grammar mistake in the application." },
                    new TicketType { Name = "Feature Suggetsion", Description = "A user has suggested a feature that could be added to the application." }
                );

            #endregion

            #region Ticket Statuses

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New", Description = "Work has not yet begun on this ticket." },
                    new TicketStatus { Name = "In Progress", Description = "Work has begun on this ticket." },
                    new TicketStatus { Name = "Under Review", Description = "This ticket is being reviewed for completion." },
                    new TicketStatus { Name = "Finished", Description = "Work has been successfully completed on this ticket." }
                );

            #endregion

            #region Ticket Priorities

            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Urgent", Description = "Ticket is of the utmost importance." },
                    new TicketPriority { Name = "High", Description = "Ticket is very important." },
                    new TicketPriority { Name = "Medium", Description = "Ticket should be handled as normal." },
                    new TicketPriority { Name = "Low", Description = "Ticket is of lesser importance." },
                    new TicketPriority { Name = "Minimal", Description = "Ticket should be handled with lowest priority." }
                );

            #endregion
        }
    }
}