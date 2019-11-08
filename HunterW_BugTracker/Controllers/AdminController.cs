using HunterW_BugTracker.Helpers;
using HunterW_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HunterW_BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private TicketsHelper tickHelper = new TicketsHelper();

        //GET: Admin
        [Authorize(Roles = "Admin, Project Manager, Demo Admin, Demo Project Manager")]
        public ActionResult AdminUserIndex()
        {
            var users = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email
            }).ToList();

            return View(users);
        }

        [Authorize(Roles = "Admin, Demo Admin")]
        //GET: User Role
        public ActionResult ManageUserRole(string userId)
        {
            //What's in this ViewBag? (List of data that is pushed into the conroller, "The column that will be used to communicate selectio to post", "The column thst we show the user inside the control", "If they already occupy a role - shot this instead of nothing")
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            return View();
        }

        [Authorize(Roles = "Admin")]
        //POST: User Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string userId, string roleName)
        {
            foreach(var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }

            if(!string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(userId, roleName);
            }
            return RedirectToAction("AdminUserIndex");
        }

        //GET: Manage User Projects
        [Authorize(Roles = "Admin, Project Manager, Demo Admin, Demo Project Manager")]
        public ActionResult ManageUserProjects(string userId)
        {
            var projects = db.Projects.ToList();
            var currentProjects = projHelper.ListUserProjects(userId);
            ViewBag.UserId = userId;
            ViewBag.ProjectNames = new MultiSelectList(db.Projects.ToList(), "Id", "Name", currentProjects);

            return View(currentProjects);
        }

        //POST: Manage User Projects
        [HttpPost]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageUserProjects(string userId, List<int> projectNames)
        {
            foreach (var project in projHelper.ListUserProjects(userId).ToList())
            {
                projHelper.RemoveUserFromProject(userId, project.Id);
            }
            if (projectNames != null)
            {
                foreach (var projectId in projectNames)
                {
                    projHelper.AddUserToProject(userId, projectId);
                }
            }
            return RedirectToAction("AdminUserIndex");
        }

        //GET: Manage User Tickets
        //[Authorize(Roles = "Admin, Project Manager")]
        //public ActionResult ManageUserTickets(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (DecisionHelper.TicketEditable(ticket))
        //    {
        //        ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
        //    }

        ////GET: Manage User Tickets
        //[Authorize(Roles = "Admin, Project Manager")]
        //public ActionResult ManageUserTickets(string userId)
        //{
        //    var tickets = db.Tickets.ToList();
        //    var currentTickets = tickHelper.ListUserTickets(userId);
        //    ViewBag.UserId = userId;
        //    ViewBag.TicketNames = new MultiSelectList(db.Tickets.ToList(), "Id", "Title", currentTickets);

        //    return View(currentTickets);
        //}

        ////POST: Manage User Tickets
        //[HttpPost]
        //[Authorize(Roles = "Admin, Project Manager")]
        //public ActionResult ManageUserTickets(string userId, List<int> ticketNames)
        //{
        //    foreach (var ticket in tickHelper.ListUserTickets(userId).ToList())
        //    {
        //        tickHelper.RemoveTicketFromUser(userId, ticket.Id);
        //    }
        //    if (ticketNames != null)
        //    {
        //        foreach (var ticketId in ticketNames)
        //        {
        //            tickHelper.AddTicketToUser(userId, ticketId);
        //        }
        //    }
        //    return RedirectToAction("AdminUserIndex");
        //}
    }
}