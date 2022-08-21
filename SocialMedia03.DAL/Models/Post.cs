using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia03.DAL.Models
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Notifs = new HashSet<Notif>();
            Reacts = new HashSet<React>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public string? Hashtag { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notif> Notifs { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual User User {get; set; }
        [NotMapped]
        public virtual int CommentSetLength { get; set; } = 0;
    }
}
