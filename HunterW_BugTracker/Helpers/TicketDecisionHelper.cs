using HunterW_BugTracker.Enumerations;
using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Helpers
{
    public class TicketDecisionHelper : CommonHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public bool TicketDetailIsViewableByUser(int tickedId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var roleName = RoleHelper.ListUserRoles(userId).FirstOrDefault();
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);

            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Submitter:
                    break;
            }
            return true;
        }

        public bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();

            switch(myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId;
                case "ProjectManager":
                    return db.Users.Find(userId).Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Admin":
                    return true;
                default:
                    return false;
            }
        }

        public static bool TicketTypeIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketStatusIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }
        public static bool TicketPriorityIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketTitleIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketDescriptionIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }

        public static bool TicketAssignedToUserIdIsEditableByUser(string userId, int ticketId)
        {
            return true;
        }
    }
}