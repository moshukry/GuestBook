using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string? Confirm_Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
