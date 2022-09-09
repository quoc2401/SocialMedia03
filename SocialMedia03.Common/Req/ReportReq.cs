using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Req
{
    [Serializable]
    public class ReportReq
    {
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public string? Reason { get; set; }
        public string? Details { get; set; }

    }
}
