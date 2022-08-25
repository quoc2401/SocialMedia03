using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Req
{
    [Serializable]
    public class ReportRequest
    {
        public int TargetUserId { get; set; }
        public int TargetPostId { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
    }
}
