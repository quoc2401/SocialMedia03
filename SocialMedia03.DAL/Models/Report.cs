using SocialMedia03.Common.DAL;
using SocialMedia03.Common.Res;
using System;
using System.Collections.Generic;

namespace SocialMedia03.DAL.Models
{
    public partial class Report : TEntity
    {
        public int UserId { get; set; }
        public int? TargetUserId { get; set; }
        public int? TargetPostId { get; set; }
        public string Reason { get; set; } = null!;
        public string? Details { get; set; }
        public bool? IsSolve { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual Post? TargetPost { get; set; }
        public virtual User? TargetUser { get; set; }
        public virtual User User { get; set; } = null!;

        
    }
}
