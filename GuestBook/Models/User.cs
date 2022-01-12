using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage ="*")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage ="Insert a valid email address!")]
        public string? UserEmail { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "*")]
        [Compare("Password", ErrorMessage ="password dosent match")]
        public string? Confirm_Password { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
