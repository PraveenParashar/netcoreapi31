using System;
using System.Collections.Generic;
using System.Text;
using Common;
using DataAccess;
using Domain;
using Microsoft.Extensions.Options;


namespace DataAccess.Repositories
{
   public class UserRepository : IUserRepository
    {
        private readonly  MyDBSetting _dBSetting;

        public UserRepository(IOptions<MyDBSetting> dBSetting)
        {
            _dBSetting = dBSetting.Value;
        }
        public IEnumerable<User> GetAllUser()
        {
           var users = new List<User>();

                users.Add(new User { UserID = 1, FirstName = "Praveen", LastName = "Parashar", JobID = 10001, Address =new Address {Country="India",City="Delhi"} });
            users.Add(new User { UserID = 2, FirstName = "Soniya", LastName = "Sharma", JobID = 10002, Address = new Address{ Country = "India", City = "Bangalore" } });

            return users;
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
