using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HunterW_BugTracker.Helpers;
using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;

namespace HunterW_BugTracker.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private TicketsHelper tickHelper = new TicketsHelper();

        // GET: Tickets
        [Authorize]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }

        // GET: Submitter's Tickets
        //[Authorize(Roles = "Submitter")]
        //public ActionResult SubmitterIndex()
        //{

        //}

        // GET: All Tickets For Projects That The User Is Assigned To
        //[Authorize(Roles = "Project Manager, Developer")]
        //public ActionResult MyProjectTickets()
        //{
        //    var userId = User.Identity.GetUserId();
        //    return View("MyProjectTickets", db.ApplicationUserProjects.Where(t => t.ApplicationUser_Id == User.Identity).ToList().OrderByDescending(t => t.Created));

        //}

        // GET: Tickets/Dashboard
        [Authorize]
        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Submitter, Demo Submitter")]
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Submitter")]
        public ActionResult Create([Bind(Include = "Id,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,Title,Description,Created")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.OwnerUserId = User.Identity.GetUserId();
                ticket.TicketStatusId = db.TicketStatuses.FirstOrDefault(t => t.Name == "New").Id;
                ticket.Created = DateTime.Now;
                ticket.Updated = DateTime.Now;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        //
        //GET: My Tickets
        [Authorize(Roles = "Developer, Demo Developer")]
        public ActionResult MyTickets()
        {
            var userId = User.Identity.GetUserId();
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();
            var myTickets = new List<Ticket>();

            myTickets = db.Tickets.Where(t => t.AssignedToUserId == userId).ToList();

            return View("Index", myTickets);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            if (TicketDecisionHelper.TicketIsEditableByUser(ticket))
            {
                ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "DisplayName", ticket.AssignedToUserId);
                ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
                ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
                ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
                return View(ticket);
            }
            else
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId,Title,Description,Created")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var original = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                //var edit = db.Tickets.Find(ticket.Id);

                db.Entry(ticket).State = EntityState.Modified;
                ticket.Updated = DateTime.Now;
                db.SaveChanges();

                var notificationHelper = new NotificationHelper();
                notificationHelper.ManageNotifications(original, ticket);

                HistoryHelper.RecordHistory(original, ticket);

                ////NotificationHelper.Manage......
                //NotificationHelper.CreateAssignmentNotification(original, ticket);
                //HistoryHelper.RecordHistory(original, edit);

                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "DisplayName", ticket.AssignedToUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);

            return View(ticket);
        }

        //// GET: Tickets/Edit/5
        //[Authorize(Roles = "Admin, Project Manager, Submitter, Developer")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);

        //    //if (User.IsInRole("Developer") && ticket.AssignedToUserId == userId)
        //    //    allowed = true;
        //    //else if (User.IsInRole("Submitter") && ticket.OwnerUserId == userId)
        //    //    allowed = true;

        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "DisplayName", ticket.AssignedToUserId);
        //    ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
        //    ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
        //    ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
        //    ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
        //    return View(ticket);
        //}

        //// POST: Tickets/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin, Project Manager")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,Title,Description,Created")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "DisplayName", ticket.AssignedToUserId);
        //        var original = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
        //        var edit = db.Tickets.Find(ticket.Id);
        //        tickHelper.AddTicketToUser(ticket.AssignedToUserId, edit.ProjectId);
        //        edit.TicketTypeId = ticket.TicketTypeId;
        //        edit.TicketPriorityId = ticket.TicketPriorityId;
        //        edit.TicketStatusId = ticket.TicketStatusId;
        //        edit.Description = ticket.Description;
        //        edit.Updated = DateTime.Now;
        //        db.Entry(ticket).State = EntityState.Modified;
        //        db.SaveChanges();
        //        projHelper.AddUserToProject(edit.AssignedToUserId, edit.ProjectId);

        //        //NotificationHelper.Manage......
        //        NotificationHelper.CreateAssignmentNotification(original, ticket);
        //        HistoryHelper.RecordHistory(original, edit);

        //        return RedirectToAction("Details", "Tickets", new { id = ticket.Id });
        //    }
        //    ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
        //    ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
        //    ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
        //    ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
        //    return View(ticket);
        //}

        // GET: Tickets/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [Authorize(Roles = "Admin, Project Manager, Developer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
