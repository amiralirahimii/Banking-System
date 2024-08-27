using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DataAccess.DTO
{
    internal class GharzolhasaneDeposit : Deposit
    {
        private static readonly int GHARZOL_HASANE_PROFIT = 0;
        public GharzolhasaneDeposit(int userId, double balance, int depositId)
        : base(userId, balance, depositId)
        { }
        public override Deposit copy()
        {
            GharzolhasaneDeposit copyDeposit = new GharzolhasaneDeposit(OwnerUserId, CurrentBalance, DepositId);
            copyDeposit.InitialBalance = InitialBalance;
            return copyDeposit;
        }
        public override void depositTheInterest(double interest)
        {

        }

        public override double calculateTheInterest(int shortTermProfitMargin)
        {
            return GHARZOL_HASANE_PROFIT;
        }

    }
}
