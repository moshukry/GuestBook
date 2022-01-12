using System;
using System.Collections.Generic;

namespace GuestBook.Models
{
    public partial class Message
    {
        public Message()
        {
            Replies = new HashSet<Reply>();
        }

        public int MessageId { get; set; }
        public string? MessageBody { get; set; }
        public DateTime MessageTime { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
