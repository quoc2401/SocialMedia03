using System;
using System.Collections.Generic;

#nullable disable

namespace SocialMedia03.DAL.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Hometown { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserRole { get; set; }
        public bool? Enable { get; set; }
    }
}
