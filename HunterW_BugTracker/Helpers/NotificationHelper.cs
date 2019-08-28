using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace HunterW_BugTracker.Helpers
{
    public class NotificationHelper : CommonHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void ManageNotifications(Ticket original, Ticket edit)
        {
            CreateAssignmentNotification(original, edit);
            CreateChangeNotification(original, edit);
        }

        public static void CreateAssignmentNotification(Ticket original, Ticket edit)
        {
            var noChange = (original.AssignedToUserId == edit.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(original.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(edit.AssignedToUserId));

            if (noChange)
                return;
            if (assignment)
                GenerateAssignmentNotification(original, edit);
            else if (unassignment)
                GenerateUnAssignmentNotification(original, edit);
            else
            {
                GenerateAssignmentNotification(original, edit);
                GenerateUnAssignmentNotification(original, edit);
            }
        }

        private static void GenerateAssignmentNotification(Ticket original, Ticket edit)
        {
            var senderId = HttpContext.Current.User.Identity.GetUserId();
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were assigned to ticket \"{edit.Title}\" on {DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}.",
                HasBeenRead = false,
                RecipientId = edit.AssignedToUserId,
                SenderId = senderId,
                NotificationBody = $"Please acknowledge that you have read this notification by marking it as read.",
                TicketId = edit.Id,
            };
            Db.TicketNotifications.Add(notification);
            Db.SaveChanges();
        }

        private static void GenerateUnAssignmentNotification(Ticket original, Ticket edit)
        {
            var senderId = HttpContext.Current.User.Identity.GetUserId();
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were unassigned from ticket \"{edit.Title}\" on {DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}.",
                HasBeenRead = false,
                RecipientId = original.AssignedToUserId,
                SenderId = senderId,
                NotificationBody = $"Please acknowledge that you have read this notification by marking it as read.",
                TicketId = edit.Id,
            };
            Db.TicketNotifications.Add(notification);
            Db.SaveChanges();
        }

        private void CreateChangeNotification(Ticket original, Ticket edit)
        {
            var messageBody = new StringBuilder();

            foreach (var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
            {
                var oldValue = Utilities.Utilities.MakeReadable(property, original.GetType().GetProperty(property).GetValue(original, null).ToString());
                var newValue = Utilities.Utilities.MakeReadable(property, edit.GetType().GetProperty(property).GetValue(edit, null).ToString());

                if (oldValue != newValue)
                {
                    messageBody.AppendLine(new String('-', 20));
                    messageBody.AppendLine($"A change was made to property: {property}.");
                    messageBody.AppendLine(new String('-', 10));
                    messageBody.AppendLine($"The previous value was: {oldValue.ToString()}.");
                    messageBody.AppendLine(new String('-', 10));
                    messageBody.AppendLine($"The new value is: {newValue.ToString()}.");
                }
            }

            if (!string.IsNullOrEmpty(messageBody.ToString()))
            {
                var message = new StringBuilder();
                message.AppendLine($"Changes were made to ticket \"{edit.Title}\".");
                message.AppendLine(messageBody.ToString());
                var senderId = HttpContext.Current.User.Identity.GetUserId();
                var notification = new TicketNotification
                {
                    TicketId = edit.Id,
                    Created = DateTime.Now,
                    Subject = $"Ticket number {edit.Id} was modified on {DateTime.Now.ToShortDateString()} at {DateTime.Now.ToShortTimeString()}.",
                    RecipientId = original.AssignedToUserId,
                    SenderId = senderId,
                    NotificationBody = message.ToString(),
                    HasBeenRead = false
                };
                Db.TicketNotifications.Add(notification);
                Db.SaveChanges();
            }
        }

        public static int GetNewUserNotificationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId && !t.HasBeenRead).Count();
        }

        public int GetAllUserNotificationCount()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId).Count();
        }

        public static List<TicketNotification> GetUnreadUserNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.Where(t => t.RecipientId == userId && !t.HasBeenRead).ToList();
        }

        public static List<TicketNotification> ListUserNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Db.TicketNotifications.ToList();
        }
    }
}