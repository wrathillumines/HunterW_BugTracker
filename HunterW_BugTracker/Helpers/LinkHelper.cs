using HunterW_BugTracker.Enumerations;
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
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(ticket.Id);
                case SystemRole.Developer:
                    return ticket.AssignedToUserId == this.CurrentUser.Id;
                case SystemRole.Submitter:
                    return ticket.OwnerUserId == CurrentUser.Id;
                default:
                    return false;
            }
        }
    }
}