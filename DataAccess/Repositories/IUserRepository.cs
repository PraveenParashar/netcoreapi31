using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
   public interface IUserRepository
    {
        IEnumerable<User> GetAllUser();
        User GetUserById(int Id);
    }
}
