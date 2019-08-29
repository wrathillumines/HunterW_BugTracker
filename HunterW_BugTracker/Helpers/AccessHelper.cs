using HunterW_BugTracker.Enumerations;
using HunterW_BugTracker.Models;
using System.Linq;

namespace HunterW_BugTracker.Helpers
{
    public class ProjectsAccessHelper : CommonHelper
    {
        public Project Project { get; set; }

        public ProjectsAccessHelper(Project project)
        {
            Project = project;
        }

        public bool UserCanArchiveProjects()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.Select(t => t.Id).Contains(this.Project.Id);
                case SystemRole.Developer:
                case SystemRole.Submitter:
                default:
                    return false;
            }
        }

        public bool UserCanEditProjectNameAndDetails()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.Select(t => t.Id).Contains(this.Project.Id);
                case SystemRole.Developer:
                case SystemRole.Submitter:
                default:
                    return false;
            }
        }
    }

    public class TicketAccessHelper : CommonHelper
    {
        private Ticket Ticket { get; set; }

        public TicketAccessHelper(Ticket ticket)
        {
            Ticket = ticket;
        }

        public bool UserCanCreateATicket()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Submitter:
                    return true;
                case SystemRole.Admin:
                case SystemRole.ProjectManager:
                case SystemRole.Developer:
                default:
                    return false;
            }
        }

        public bool UserCanViewTicketDetails()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(Ticket.Id);
                case SystemRole.Developer:
                    return Ticket.AssignedToUserId == this.CurrentUser.Id;
                case SystemRole.Submitter:
                    return Ticket.OwnerUserId == this.CurrentUser.Id;
                default:
                    return false;
            }
        }

        public bool UserCanEditTicketStatus()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(Ticket.Id);
                case SystemRole.Developer:
                case SystemRole.Submitter:
                default:
                    return false;
            }
        }

        public bool UserCanEditTicketPriority(string userId, int ticketId)
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(Ticket.Id);
                case SystemRole.Developer:
                    return Ticket.AssignedToUserId == this.CurrentUser.Id;
                case SystemRole.Submitter:
                    return Ticket.OwnerUserId == this.CurrentUser.Id;
                default:
                    return false;
            }
        }

        public bool UserCanEditTicketTypeTitleAndDescription()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(Ticket.Id);
                case SystemRole.Developer:
                    return Ticket.AssignedToUserId == this.CurrentUser.Id;
                case SystemRole.Submitter:
                    return Ticket.OwnerUserId == this.CurrentUser.Id;
                default:
                    return false;
            }
        }

        public bool UserCanEditTicketAssignedTo()
        {
            switch (this.CurrentRole)
            {
                case SystemRole.Admin:
                    return true;
                case SystemRole.ProjectManager:
                    return this.CurrentUser.Projects.SelectMany(t => t.Tickets).Select(t => t.Id).Contains(Ticket.Id);
                case SystemRole.Developer:
                case SystemRole.Submitter:
                default:
                    return false;
            }
        }
    }
}