using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.Repository;

namespace BankingSystem.DataAccess.DTO
{
    internal class LongTermDeposit : Deposit
    {
        public LongTermDeposit(int userId, double balance, int shortTermDepositId, int years, int depositId)
            : base(userId, balance, depositId)
        {
            LinkedShortTermDepositId = shortTermDepositId;
            Years = years;
        }
        public int LinkedShortTermDepositId { get; set; }
        public int Years { get; set; }
        public override Deposit copy()
        {
            LongTermDeposit copyDeposit = new LongTermDeposit(OwnerUserId, CurrentBalance, LinkedShortTermDepositId, Years, DepositId);
            copyDeposit.InitialBalance = InitialBalance;
            return copyDeposit;
        }
        public override void depositTheInterest(double interest)
        {
            Deposit deposit = DepositRepository.Instance.getDepositById(LinkedShortTermDepositId);
            deposit.CurrentBalance += interest;
            DepositRepository.Instance.updateDeposit(deposit);
        }

        public override double calculateTheInterest(int shortTermProfitMargin)
        {
            return Years * shortTermProfitMargin * InitialBalance / 100;
        }
    }
}
