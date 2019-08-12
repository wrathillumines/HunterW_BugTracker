using System;

namespace HunterW_BugTracker.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string AttachmentUrl { get; set; }

        //virtual

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
        public class TicketComment
        {
            public int Id { get; set; }
            public int TicketId { get; set; }
            public string UserId { get; set; }
            public string CommentBody { get; set; }
            public DateTime Created { get; set; }
        }
    }
}