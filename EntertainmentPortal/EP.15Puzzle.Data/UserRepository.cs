using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Data
{
    public static class UserRepository
    {
        static List<User> _users = new List<User>(){new User(){Id=1,Name = "1",Country = "Belarus"}};

        public static User Get(int id)
        {
            return _users.Find(u => u.Id == id);
        }

        public static bool Create(User user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
                return true;
            }
            return false;
        }
        
        public static void Delete(int id)
        {
            _users.Remove(_users.Find(u => u.Id == id));
        }
    }
}
