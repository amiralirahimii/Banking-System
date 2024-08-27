using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.DTO;

namespace BankingSystem.DataAccess.Repository
{
    internal class UserRepository
    {
        private static readonly UserRepository _instance = new UserRepository();
        private UserRepository()
        {
            _userList = new List<User>();
        }

        public static UserRepository Instance
        {
            get
            {
                return _instance;
            }
        }
        private List<User> _userList;
        public bool doesUserExists(int userId)
        {
            return _userList.Any(user => userId == user.Id);
        }
        public User getUserById(int userId)
        {
            User targetUser = _userList.Single(user => user.Id == userId);
            return targetUser.copy();
        }
        public void updateUser(User updatedUser)
        {
            int index = _userList.FindIndex(user => user.Id == updatedUser.Id);
            _userList[index] = updatedUser;
        }
        public void AddUser(User user)
        {
            _userList.Add(user);
        }

    }
}
