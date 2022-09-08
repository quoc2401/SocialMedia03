using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Req
{
    [Serializable]
    public class CommentReq
    {
        public string Content { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
    }
}
