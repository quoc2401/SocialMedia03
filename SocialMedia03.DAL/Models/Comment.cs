using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia03.DAL.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
            Notifs = new HashSet<Notif>();
            Reacts = new HashSet<React>();
        }

        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ParentId { get; set; }

        public virtual Comment? Parent { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> InverseParent { get; set; }
        public virtual ICollection<Notif> Notifs { get; set; }
        public virtual ICollection<React> Reacts { get; set; }

        [NotMapped]
        public virtual int CommentSetLength { get; set; } = 0;
    }
}
