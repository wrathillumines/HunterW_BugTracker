using HunterW_BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HunterW_BugTracker.Helpers
{
    public class HistoryHelper : CommonHelper
    {
        public static void RecordHistory(Ticket original, Ticket edit)
        {
            foreach (var property in WebConfigurationManager.AppSettings["TrackedHistoryProperties"].Split(','))
            {
                var oldValue = original.GetType().GetProperty(property).GetValue(original, null).ToString();
                var newValue = edit.GetType().GetProperty(property).GetValue(edit, null).ToString();

                if(oldValue != newValue)
                {
                    var newHistory = new TicketHistory
                    {
                        UserId = HttpContext.Current.User.Identity.GetUserId(),
                        Updated = (DateTime)edit.Updated,
                        PropertyName = property,
                        OldValue = oldValue,
                        NewValue = newValue,
                        TicketId = edit.Id
                    };
                    Db.TicketHistories.Add(newHistory);
                }
            }
            Db.SaveChanges();
        }
    }
}