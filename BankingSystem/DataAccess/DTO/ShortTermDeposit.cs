using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.Repository;

namespace BankingSystem.DataAccess.DTO
{
    internal class ShortTermDeposit : Deposit
    {
        public ShortTermDeposit(int userId, double balance, int id, int depositId)
        : base(userId, balance, depositId)
        {
            Id = id;
        }
        public int Id { get; set; }
        public override Deposit copy()
        {
            ShortTermDeposit copyDeposit = new ShortTermDeposit(OwnerUserId, CurrentBalance, Id, DepositId);
            copyDeposit.InitialBalance = InitialBalance;
            return copyDeposit;
        }
        public override void depositTheInterest(double interest)
        {
            Deposit deposit = DepositRepository.Instance.getDepositById(DepositId);
            deposit.CurrentBalance += interest;
            DepositRepository.Instance.updateDeposit(deposit);
        }
        public override double calculateTheInterest(int shortTermProfitMargin)
        {
            return shortTermProfitMargin * InitialBalance / 100;
        }
    }
}
