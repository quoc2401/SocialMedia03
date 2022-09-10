using SocialMedia03.Common.BLL;
using SocialMedia03.Common.Res;
using SocialMedia03.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.BLL
{
    public class NotifSvc: GenericSvc<NotifRep, Notif>
    {
        public List<NotifRes> getNotifs(int userId, int page)
        {
            return _rep.getNotifs(userId, page);
        }

        public bool readNotif(int notifId)
        {
            Notif n = _rep.GetSingle<Notif>(notifId);
            if (n != null)
                n.IsRead = true;
            return _rep.Update(n);
        }
    }
}
