using System;
using System.Collections.Generic;

#nullable disable

namespace SocialMedia03.DAL.Models
{
    
    public partial class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserId { get; set; }
        public string Hashtag { get; set; }
    }
}
