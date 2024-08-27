using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DataAccess.DTO
{
    internal abstract class Deposit
    {
        public Deposit(int userId, double balance, int depositId)
        {
            DepositId = depositId;
            InitialBalance = balance;
            CurrentBalance = balance;
            OwnerUserId = userId;
        }

        public int DepositId { get; set; }
        public double InitialBalance { get; set; }
        public double CurrentBalance { get; set; }
        public int OwnerUserId { get; set; }
        public abstract Deposit copy();
        public abstract void depositTheInterest(double interest);
        public abstract double calculateTheInterest(int shortTermProfitMargin);
    }
}
