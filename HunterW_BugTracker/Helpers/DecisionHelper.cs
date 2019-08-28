using HunterW_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HunterW_BugTracker.Helpers
{
    public class DecisionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserRolesHelper rolesHelper = new UserRolesHelper();
        private static ProjectsHelper projectsHelper = new ProjectsHelper();

        //public static bool TicketDetailIsViewableByUser(int ticketId)
        //{
        //  
        //}

        //public static bool TicketIsEditableByUser(Ticket ticket)
        //{
            
        //}
    }
}