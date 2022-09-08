﻿using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using SocialMedia03.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common.Res;
using Microsoft.Data.SqlClient;

namespace SocialMedia03.DAL
{
    public class ReportRep: GenericRep<smdbContext, Report>
    {
        public ReportRep()
        { }

        //override
        public List<Report> GetReport()
        {
            List<Report> res = new List<Report>();
            res = base.Context.Set<Report>().ToList<Report>();
            
            return res; 
        }


        public bool Create(React r)
        {
            try
            {
                base.Context.Entry(r).State = r.Id == 0 ?
                    EntityState.Added : EntityState.Modified;
                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

        }
        public bool CreateReport(Report report)
        {
            try
            {
                base.Context.Entry(report).State = report.Id == 0 ?
                    EntityState.Added : EntityState.Modified;
                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }


    }
}