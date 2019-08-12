using System;

namespace HunterW_BugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public DateTime Created { get; set; }
        public string NotificationBody { get; set; }
        public string Subject { get; set; }
        public bool HasBeenRead { get; set; }

        //virtual

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
        public virtual ApplicationUser Sender { get; set; }
    }
}