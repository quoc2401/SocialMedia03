using SocialMedia03.Common.DAL;
using System;
using System.Collections.Generic;

namespace SocialMedia03.DAL.Models
{
    public partial class React : TEntity
    {
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public short Type { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
