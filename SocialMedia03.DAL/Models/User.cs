using SocialMedia03.Common.DAL;
using SocialMedia03.WEB.Models;
using System;
using System.Collections.Generic;
using BC = BCrypt.Net.BCrypt;

namespace SocialMedia03.DAL.Models
{
    public partial class User : TEntity
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Notifs = new HashSet<Notif>();
            Posts = new HashSet<Post>();
            Reacts = new HashSet<React>();
            ReportTargetUsers = new HashSet<Report>();
            ReportUsers = new HashSet<Report>();
        }
        public User(UserReq req)
        {
            Email = req.Email;
            Firstname = req.Firstname;
            Lastname = req.Lastname;
            Birthday = req.Birthday;
            Password = BC.HashPassword(req.Password);
            Address = req.Address;
            Hometown = req.Hometown;
            Phone = req.Phone;
            Avatar = req.Avatar;
        }
        public string Uuid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? Hometown { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserRole { get; set; } = null!;
        public bool? Enable { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Notif> Notifs { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<React> Reacts { get; set; }
        public virtual ICollection<Report> ReportTargetUsers { get; set; }
        public virtual ICollection<Report> ReportUsers { get; set; }
    }
}
