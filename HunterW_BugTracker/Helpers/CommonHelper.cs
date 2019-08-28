using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Helpers
{
    public abstract class CommonHelper
    {
        protected static ApplicationDbContext Db = new ApplicationDbContext();
        protected UserRolesHelper RoleHelper = new UserRolesHelper();
        protected ProjectsHelper ProjectHelper = new ProjectsHelper();
        protected ApplicationUser CurrentUser = new ApplicationUser();
        protected String CurrentRole = "";

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            this.CurrentUser = Db.Users.Find(userId);
            this.CurrentRole = RoleHelper.ListUserRoles(this.CurrentUser.Id).FirstOrDefault();
        }
    }
}