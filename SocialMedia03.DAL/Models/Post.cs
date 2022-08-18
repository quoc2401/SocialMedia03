using System;
using System.Collections.Generic;

namespace SocialMedia03.DAL.Models
{
    public partial class Post
    {
        public Post()
        {
            Notifs = new HashSet<Notif>();
            Reacts = new HashSet<React>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public string? Hashtag { get; set; }

        public virtual ICollection<Notif> Notifs { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
