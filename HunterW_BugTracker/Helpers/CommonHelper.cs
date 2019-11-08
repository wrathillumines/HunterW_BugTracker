using HunterW_BugTracker.Enumerations;
using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Helpers
{
    public abstract class CommonHelper
    {
        protected static ApplicationDbContext Db = new ApplicationDbContext();
        protected static UserRolesHelper RoleHelper = new UserRolesHelper();
        protected ProjectsHelper ProjectHelper = new ProjectsHelper();
        protected ApplicationUser CurrentUser = new ApplicationUser();
        protected SystemRole CurrentRole = SystemRole.None;

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
                CurrentUser = Db.Users.Find(userId);

            var stringRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            if (!string.IsNullOrEmpty(stringRole))
                CurrentRole = (SystemRole)Enum.Parse(typeof(SystemRole), stringRole);
        }
    }
}