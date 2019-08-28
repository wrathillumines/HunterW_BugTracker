using HunterW_BugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HunterW_BugTracker.Helpers
{
    public class TicketsHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public bool IsUserOnTicket(string userId, int ticketId)
        {
            var ticket = db.Tickets.Find(ticketId);
            var flag = ticket.Users.Any(u => u.Id == userId);
            return (flag);
        }

        public ICollection<Ticket> ListUserTickets(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);

            var tickets = user.Tickets.ToList();
            return (tickets);
        }

        public void AddTicketToUser(string userId, int ticketId)
        {
            //var user = db.Users.Find(userId);
            //Ticket tick = db.Tickets.Find(ticketId);

            //tick.Project.Users.Add(user);
            //db.SaveChanges();

            if (!IsUserOnTicket(userId, ticketId))
            {
                Ticket tick = db.Tickets.Find(ticketId);
                var user = db.Users.Find(userId);

                tick.Project.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void RemoveTicketFromUser(string userId, int ticketId)
        {
            if (IsUserOnTicket(userId, ticketId))
            {
                Ticket tick = db.Tickets.Find(ticketId);
                var delUser = db.Users.Find(userId);

                tick.Project.Users.Remove(delUser);
                db.Entry(tick).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> UsersOnTicket(int ticketId)
        {
            return db.Tickets.Find(ticketId).Users;
        }

        public ICollection<ApplicationUser> UsersNotOnTicket(int ticketId)
        {
            return db.Users.Where(u => u.Tickets.All(p => p.Id != ticketId)).ToList();

        }
    }
}