using SocialMedia03.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Res
{
    public class NotifRes
    {
        public int TargetId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NotifType Type { get; set; }
        public bool IsRead { get; set; }
        public int Count { get; set; }
        public string LastModifiedName { get; set; }
        public string LastModifiedAvatar { get; set; }
        public DateTime LastModified { get; set; }
        public int NotifId { get; set; }

    }
}
