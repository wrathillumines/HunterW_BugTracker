using HunterW_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Helpers
{
    public class LinkHelper : CommonHelper
    {
        public bool UserCanEditTicket(Ticket ticket)
        {
            switch(this.CurrentRole)
            {
                case "Admin":
                    return true;
                case "Project Manager":
                    return CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case "Developer":
                    return ticket.AssignedToUserId == this.CurrentUser.Id;
                case "Submitter":
                    return ticket.OwnerUserId == CurrentUser.Id;
                default:
                    return false;
            }
        }
    }
}