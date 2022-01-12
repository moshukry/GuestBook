using System;
using System.Collections.Generic;

namespace GuestBook.Models
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        public string? ReplyBody { get; set; }
        public DateTime ReplyTime { get; set; }
        public int? MessageId { get; set; }

        public virtual Message? Message { get; set; }
    }
}
