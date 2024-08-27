using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.DTO;
using BankingSystem.DataAccess.Repository;

namespace BankingSystem.Service
{
    internal class BankService
    {
        public BankService() { }
        private void createDepositProcedure(User user, Bank bank, Deposit newDeposit, double initialInvestment)
        {
            DepositRepository.Instance.AddDeposit(newDeposit);
            user.AddDepositId(newDeposit.DepositId);
            bank.AddDepositId(newDeposit.DepositId);
            user.Wallet -= initialInvestment;
            UserRepository.Instance.updateUser(user);
            BankRepository.Instance.updateBank(bank);
        }
        public int createShortTermDeposit(int userId, int bankId, double initialInvestment)
        {
            User user = UserRepository.Instance.getUserById(userId);
            Bank bank = BankRepository.Instance.getBankById(bankId);
            ShortTermDeposit shortTermDeposit = new ShortTermDeposit(userId, initialInvestment, bank.GetNextShortTermDepositId(), DepositRepository.Instance.GetNextDepositId());
            createDepositProcedure(user, bank, shortTermDeposit, initialInvestment);
            return shortTermDeposit.Id;
        }
        public void createLongTermDeposit(int userId, int bankId, int shortTermDepositId, int years, double initialInvestment)
        {
            User user = UserRepository.Instance.getUserById(userId);
            Bank bank = BankRepository.Instance.getBankById(bankId);
            Deposit shortTermDeposit = user.associatedDepositList
                .Intersect(bank.DepositList)
                .Select(depositId => DepositRepository.Instance.getDepositById(depositId))
                .OfType<ShortTermDeposit>()
                .Single(deposit => deposit.Id == shortTermDepositId);

            LongTermDeposit longTermDeposit = new LongTermDeposit(userId, initialInvestment, shortTermDeposit.DepositId, years, DepositRepository.Instance.GetNextDepositId());
            createDepositProcedure(user, bank, longTermDeposit, initialInvestment);
        }
        public void createGharzolhasaneDeposit(int userId, int bankId, double initialInvestment)
        {
            User user = UserRepository.Instance.getUserById(userId);
            Bank bank = BankRepository.Instance.getBankById(bankId);
            GharzolhasaneDeposit gharzolhasaneDeposit = new GharzolhasaneDeposit(userId, initialInvestment, DepositRepository.Instance.GetNextDepositId());
            createDepositProcedure(user, bank, gharzolhasaneDeposit, initialInvestment);
        }
        private void calculateAndDepositInterestForOneMonth(int bankId)
        {
            Bank bank = BankRepository.Instance.getBankById(bankId);
            foreach (int depositId in bank.DepositList)
            {
                Deposit deposit = DepositRepository.Instance.getDepositById(depositId);
                double interest = deposit.calculateTheInterest(bank.ShortTermProfitMargin);
                deposit.depositTheInterest(interest);
            }
        }
        public void calculateAndDepositInterestForGivenMonths(int bankId, int months)
        {
            for (int i = 0; i < months; i++)
            {
                calculateAndDepositInterestForOneMonth(bankId);
            }
        }
        public double getUserBalanceInCertainShortTermDeposit(int userId, int bankId, int shortTermDepositId)
        {
            Bank bank = BankRepository.Instance.getBankById(bankId);
            User user = UserRepository.Instance.getUserById(userId);
            return user.associatedDepositList
                .Intersect(bank.DepositList)
                .Select(depositId => DepositRepository.Instance.getDepositById(depositId))
                .OfType<ShortTermDeposit>()
                .Where(deposit => deposit.Id == shortTermDepositId)
                .Select(deposit => deposit.CurrentBalance)
                .Single();
        }
        public double getUserBalanceInCertainBank(int userId, int bankId)
        {
            Bank bank = BankRepository.Instance.getBankById(bankId);
            User user = UserRepository.Instance.getUserById(userId);
            return user.associatedDepositList
                .Intersect(bank.DepositList)
                .Select(depositId => DepositRepository.Instance.getDepositById(depositId).CurrentBalance)
                .Sum();
        }

        public double getUserTotalBalance(int userId)
        {
            User user = UserRepository.Instance.getUserById(userId);
            return user.associatedDepositList
               .Select(depositId => DepositRepository.Instance.getDepositById(depositId).CurrentBalance)
               .Sum();
        }
    }
}
