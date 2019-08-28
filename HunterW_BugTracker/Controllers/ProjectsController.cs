using HunterW_BugTracker.Helpers;
using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HunterW_BugTracker.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper projHelper = new ProjectsHelper();
        private UserRolesHelper rolesHelper = new UserRolesHelper();

        // GET: All Projects
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: User's Assigned Projects
        [Authorize]
        public ActionResult UserIndex()
        {
            var userId = User.Identity.GetUserId();
            var allProjects = projHelper.ListUserProjects(userId);

            return View(allProjects.ToList());
        }

        [Authorize]
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //var allProjectManagers = roleHelper.UsersInRole("Project Manager");
        //ViewBag.ProjectManagers = new MultiSelectList(allProjectManagers, "Id", "FullNameWithEmail");

        ////repeat above var and shit to the two lines below
        //Viewbag.Submitters = MultiSelectList();
        //ViewBag.Developers = new MultiSelectList();

        // GET: Projects/Create
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Project Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Created = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize(Roles = "Admin, Project Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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

        //GET: Submitter Project Index
        [Authorize(Roles = "Submitter")]
        public ActionResult SubmitterProjectIndex(int? id)
        {
            Project project = db.Projects.Find(id);
            ViewBag.Tickets = db.Tickets.ToList();

            if (project == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        ////GET: Manage Project Users
        //public ActionResult ManageProjectUsers(int projectId)
        //{
        //    var user = db.Users.ToList();
        //    var currentUsers = projHelper.UsersOnProject(projectId);
        //    ViewBag.ProjectId = projectId;
        //    ViewBag.UserNames = new MultiSelectList(db.Users.ToList(), "Id", "Name", currentUsers);
        //    return View(currentUsers);
        //}

        ////POST: Manage Project Users
        //[HttpPost]
        //public ActionResult ManageProjectUsers(int projectId, List<string> userNames)
        //{
        //    foreach (var user in projHelper.UsersOnProject(projectId).ToList())
        //    {
        //        projHelper.RemoveUserFromProject(userId, project.Id);
        //    }
        //}

        ////GET: EditUser
        //public ActionResult EditUser(string id)
        //{
        //    var user = db.Users.Find(id);
        //    AdminUserViewModel AdminModel = new AdminUserViewModel();
        //    UserRolesHelper helper = new UserRolesHelper();
        //    var selected = helper.ListUserRoles(id);
        //    AdminModel.Roles = new MultiSelectList(db.Roles, "Name", "Name", selected);
        //    AdminModel.Id = user.Id;
        //    AdminModel.Name = user.DisplayName;

        //    return View(AdminModel);
        //}

        ////POST: EditUser
        //public ActionResult EditUser(AdminUserViewModel model)
        //{
        //    var user = db.Users.Find(model.Id);
        //    UserRolesHelper helper = new UserRolesHelper();
        //    foreach(var roleremove in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.RemoveUserFromRole(user.Id, roleremove);
        //    }
        //    foreach(var roleadd in db.Roles.Select(r => r.Id).ToList())
        //    {
        //        helper.AddUserToRole(user.Id, roleadd);
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
