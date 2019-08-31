using HunterW_BugTracker.ChartViewModels;
using HunterW_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HunterW_BugTracker.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Charts
        public JsonResult GetHardCodedMorrisBarData()
        {
            var dataSet = new List<MorrisBarChartData>();
            dataSet.Add(new MorrisBarChartData { label = "Minimal", value = 3 });
            dataSet.Add(new MorrisBarChartData { label = "Low", value = 7 });
            dataSet.Add(new MorrisBarChartData { label = "Medium", value = 1 });
            dataSet.Add(new MorrisBarChartData { label = "High", value = 14 });
            dataSet.Add(new MorrisBarChartData { label = "Maximum", value = 9 });

            return Json(dataSet);
        }

        public JsonResult MorrisPriorityData()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach(var ticketPriority in db.TicketPriorities.ToList())
            {
                var value = db.TicketPriorities.Find(ticketPriority.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketPriority.Name, value = value });
            }

            return Json(dataSet);
        }

        public JsonResult MorrisCategoryData()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach (var ticketType in db.TicketTypes.ToList())
            {
                var value = db.TicketTypes.Find(ticketType.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketType.Name, value = value });
            }

            return Json(dataSet);
        }

        public JsonResult MorrisStatusData()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach (var ticketStatus in db.TicketStatuses.ToList())
            {
                var value = db.TicketStatuses.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = ticketStatus.Name, value = value });
            }

            return Json(dataSet);
        }

        public JsonResult MorrisProjectTicketsData()
        {
            var dataSet = new List<MorrisBarChartData>();

            foreach (var project in db.Projects.ToList())
            {
                var value = db.Projects.Find(project.Id).Tickets.Count();
                dataSet.Add(new MorrisBarChartData { label = project.Name, value = value });
            }

            return Json(dataSet);
        }
    }
}