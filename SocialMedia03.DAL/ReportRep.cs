using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using SocialMedia03.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common.Res;

namespace SocialMedia03.DAL
{
    public class ReportRep: GenericRep<smdbContext, Report>
    {
        public ReportRep()
        { }

        //override
        public override Report Get(int id)
        {
            var res = All.FirstOrDefault(i => i.TargetUserId == id);
            return res;
        }
        
        public SingleRes CreateReport(Report report)
        {
            SingleRes res = new SingleRes();
            using (var context = new smdbContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Reports.Add(report);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }


    }
}
