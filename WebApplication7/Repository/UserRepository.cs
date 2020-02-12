using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.DataBase;
using WebApplication7.Models;

namespace WebApplication7.Repository
{
    public class UserRepository : IUserRepository
    {
        private DB db = new DB();

        public IQueryable<User> Users
        {
            get { return db.Users; }
        }
        public void SaveUser(User user)
        {
            db.AddUser(user);
        }
    }
}
