using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Req
{
    [Serializable]
    public class PostReq
    {
        public string Content { get; set; }
        public string Hashtag { get; set; }
        public string ImageUrl { get; set; }
    }
}
