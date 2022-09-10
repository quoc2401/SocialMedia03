using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMedia03.Common;
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
    public class UserRep : GenericRep<smdbContext, User>
    {
        public User GetUserByPost(int postId)
        {
            var query = from user in Context.Users
                        join post in Context.Posts on user.Id equals post.UserId
                        where post.Id == postId
                        select user;
            return query.FirstOrDefault<User>();
        }

        public User getUserByComment(int commentId)
        {
            var query = from user in Context.Users
                        join comment in Context.Comments on user.Id equals comment.UserId
                        where comment.Id == commentId
                        select user;
            return query.FirstOrDefault<User>();
        }

        public User GetUserByEmail(string email)
        {
            return base.Context.Set<User>().Where(u => u.Email == email).SingleOrDefault();
        }

        public int CountUser(int month, int year)
        {
            int count;
            if (year == 0)
                count = Context.Users.Count();
            else if (month >= 1 && month <= 12)
                count = Context.Users.Where(u => u.CreatedDate.Month == month && u.CreatedDate.Year == year).Count();
            else
                count = Context.Users.Where(u => u.CreatedDate.Year == year).Count();
            return count;
        }

        public List<User> SearchByUser(string kw, int page, int limit)
        {
            List<User> rs = new List<User>();
            int size = limit == 0 ? configs.POST_PAGE_SIZE : limit;
            if (kw == null)
                kw = "";

            if (page > 0)
            {
                int start = (page - 1) * size;

                rs = base.Context.Set<User>().Where(u => u.Firstname.Contains(kw) || u.Lastname.Contains(kw)).AsEnumerable().Skip(start).Take(size).ToList();
            }
            else
            {
                rs = base.Context.Set<User>().Where(u => u.Firstname.Contains(kw) || u.Lastname.Contains(kw)).ToList();
            }

            return rs;
        }

    }
}