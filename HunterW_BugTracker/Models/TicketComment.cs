using System;

namespace HunterW_BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string CommentBody { get; set; }
        public string AuthorId { get; set; }
        public DateTimeOffset Created { get; set; }

        //virtual
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}