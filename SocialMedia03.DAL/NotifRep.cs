using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common;
using SocialMedia03.Common.DAL;
using SocialMedia03.Common.Res;
using SocialMedia03.Common.Utils;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class NotifRep: GenericRep<smdbContext, Notif>
    {
        public List<NotifRes> getNotifs(int userId, int page)
        {
            List<NotifRes> rs = new List<NotifRes>();
            int size = configs.NOTIF_PAGE_SIZE;
            if (page > 0)
            {
                int start = (page - 1) * size;

                //rs = base.Context.Database($"EXEC sp_userGetNotifs {userId}, {start}, {size}").ToHashSet();
                var command = base.Context.Database.GetDbConnection().CreateCommand();
                command.CommandText = $"EXEC sp_userGetNotifs {userId}, {start}, {size}";
                base.Context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        NotifRes r = new NotifRes();
                        r.TargetId = result.GetInt32("target_id");
                        r.Type = Enum.Parse<NotifType>(result.GetString("type"));
                        r.IsRead = result.GetBoolean("is_read");
                        r.Count = result.GetInt32("count");
                        r.LastModifiedName = result.GetString("last_modified_name");
                        r.LastModifiedAvatar = result.GetString("last_modified_avatar");
                        r.LastModified = result.GetDateTime("last_modified");
                        r.NotifId = result.GetInt32("notif_id");
                        rs.Add(r);
                    }
                }
            }
            return rs;
        }
    }
}
