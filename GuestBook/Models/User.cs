using System;
using System.Collections.Generic;

namespace GuestBook.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
