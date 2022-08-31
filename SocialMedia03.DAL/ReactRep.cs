using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common.DAL;
using SocialMedia03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.DAL
{
    public class ReactRep : GenericRep<smdbContext, React>
    {
        public override React Get(int id)
        {
            return base.Context.Set<React>().Where(r => r.Id == id).SingleOrDefault();
        }

        public HashSet<React> GetReactsByPost(int postId) 
        {
            return base.Context.Set<React>().Where(r => r.PostId == postId).ToHashSet();
        }

        public HashSet<React> GetReactsByComment(int commentId)
        {

            return base.Context.Set<React>().Where(r => r.CommentId == commentId).ToHashSet();
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
                Debug.WriteLine(e.StackTrace);
                return false;
            }

        }

        public bool Delete(React r)
        {
            if (r == null)
                return true;
            try
            {
                base.Context.Entry(r).State = EntityState.Deleted;

                base.Context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        public React GetReactPost(int postId, int userId)
        {
            return base.Context.Set<React>().Where(r => r.PostId==postId && r.UserId==userId).SingleOrDefault();
        }

        public React GetReactComment(int commentId, int userId)
        {
            return base.Context.Set<React>().Where(r => r.CommentId == commentId && r.UserId == userId).SingleOrDefault();
        }
        public int CountReact(int month, int year)
        {
            int count;
            if (year == 0)
                count = Context.Reacts.Count();
            else if (month >= 1 && month <= 12)
                count = Context.Reacts.Where(u => u.CreatedDate.Month == month && u.CreatedDate.Year == year).Count();
            else
                count = Context.Reacts.Where(u => u.CreatedDate.Year == year).Count();
            return count;
        }
    }
}
