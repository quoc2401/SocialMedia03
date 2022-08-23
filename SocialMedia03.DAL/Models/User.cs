using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SocialMedia03.DAL.Models
{
    public partial class User
    {
        
        public User()
        {
            Comments = new HashSet<Comment>();
            Notifs = new HashSet<Notif>();
            Reacts = new HashSet<React>();
            ReportTargetUsers = new HashSet<Report>();
            ReportUsers = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string? Uuid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? Hometown { get; set; }
        public string? Phone { get; set; }
        public string Avatar { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? UserRole { get; set; } = null!;
        public bool? Enable { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notif> Notifs { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual ICollection<Report> ReportTargetUsers { get; set; }
        public virtual ICollection<Report> ReportUsers { get; set; }
    }
}
