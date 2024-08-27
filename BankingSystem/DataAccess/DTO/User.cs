using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DataAccess.DTO
{
    internal class User
    {
        public User(int id, double wallet)
        {
            Id = id;
            Wallet = wallet;
        }
        public int Id { get; set; }
        public double Wallet { get; set; }
        public List<int> associatedDepositList { get; set; }
        public User copy()
        {
            User copyUser = new User(Id, Wallet);
            if (associatedDepositList != null)
                copyUser.associatedDepositList = new List<int>(associatedDepositList);
            else
                copyUser.associatedDepositList = new List<int>();
            return copyUser;
        }
        public void AddDepositId(int depositId)
        {
            associatedDepositList.Add(depositId);
        }
    }
}
