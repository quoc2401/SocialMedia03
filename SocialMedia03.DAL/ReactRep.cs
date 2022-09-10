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
        //public override React Get(int id)
        //{
        //    return base.Context.Set<React>().Where(r => r.Id == id).SingleOrDefault();
        //}

        public IQueryable<React> GetReactsByPost(int postId) 
        {
            return base.Get<React>(r => r.PostId == postId);
        }

        public IQueryable<React> GetReactsByComment(int commentId)
        {

            return base.Get<React>(r => r.CommentId == commentId);
        }

        public React GetReactPost(int postId, int userId)
        {
            return base.GetSingle<React>(r => r.PostId==postId && r.UserId==userId);
        }

        public React GetReactComment(int commentId, int userId)
        {
            return base.GetSingle<React>(r => r.CommentId == commentId && r.UserId == userId);
        }

        public int CountReact(int month, int year)
        {
            int count;
            if (year == 0)
                count = Context.Reacts.Count();
            else if (month >= 1 && month <= 12)
                count = base.Get<React>(u => u.CreatedDate.Month == month && u.CreatedDate.Year == year).Count();
            else
                count = base.Get<React>(u => u.CreatedDate.Year == year).Count();
            return count;
        }
    }
}
