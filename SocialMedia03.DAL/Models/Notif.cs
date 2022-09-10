using SocialMedia03.Common.DAL;
using System;
using System.Collections.Generic;

namespace SocialMedia03.DAL.Models
{
    public partial class Notif : TEntity
    {
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string Type { get; set; } = null!;
        public bool? IsRead { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
