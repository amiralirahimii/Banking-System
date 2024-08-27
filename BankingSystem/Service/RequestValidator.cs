using BankingSystem.Communication;
using BankingSystem.DataAccess.DTO;
using BankingSystem.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service
{
    internal class RequestValidator
    {
        public RequestValidator()
        {

        }
        public ValidatorAppServiceMessage validateCreateShortTermDeposit(int userId, int bankId, double initialInvestment)
        {
            if (BankRepository.Instance.getBankById(bankId).ShortTermMinimumInvestemnt > initialInvestment ||
                UserRepository.Instance.getUserById(userId).Wallet < initialInvestment)
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.NOT_ENOUGH_MONEY);
            else
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validateCreateLongTermDeposit(int userId, int bankId, int shortTermDepositId, int years, double initialInvestment)
        {
            User user = UserRepository.Instance.getUserById(userId);
            Bank bank = BankRepository.Instance.getBankById(bankId);
            if (BankRepository.Instance.getBankById(bankId).ShortTermMinimumInvestemnt*years > initialInvestment ||
                UserRepository.Instance.getUserById(userId).Wallet < initialInvestment)
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.NOT_ENOUGH_MONEY);
            else if (
                user.associatedDepositList
                .Intersect(bank.DepositList)
                .Select(depositId => DepositRepository.Instance.getDepositById(depositId))
                .OfType<ShortTermDeposit>()
                .Where(deposit => deposit.Id == shortTermDepositId)
                .FirstOrDefault() == null
                )
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.INVALID_SHORT_TERM_DEPOSIT);
            else
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validateCreateGharzolhasaneDeposit(int userId, int bankId, double initialInvestment)
        {
            if (UserRepository.Instance.getUserById(userId).Wallet < initialInvestment)
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.NOT_ENOUGH_MONEY);
            else
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validatePassTime(int months)
        {
            return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validateInventoryReport(int userId, int bankId, int shortTermDepositId)
        {
            User user = UserRepository.Instance.getUserById(userId);
            Bank bank = BankRepository.Instance.getBankById(bankId);
            if(
                user.associatedDepositList
                .Intersect(bank.DepositList)
                .Select(depositId => DepositRepository.Instance.getDepositById(depositId))
                .OfType<ShortTermDeposit>()
                .Where(deposit => deposit.Id == shortTermDepositId)
                .SingleOrDefault() == null
            )
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.INVALID_SHORT_TERM_DEPOSIT);
            else
                return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validateCalcMoneyInBank(int userId, int bankId)
        {
            return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
        public ValidatorAppServiceMessage validateCalcAllMoney(int userId)
        {
            return new ValidatorAppServiceMessage(ValidatorAppServiceMessage.MISSION_SUCCESSFUL);
        }
    }
}
