using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Communication;
using BankingSystem.DataAccess.DTO;
using BankingSystem.DataAccess.Repository;

namespace BankingSystem.Service
{
    internal class ApplicationService
    {
        private static readonly BankService _bankService = new BankService();
        private static readonly RequestValidator _requestValidator = new RequestValidator();
        public ApplicationService(){}
        public AppServiceUIMessage createShortTermDeposit(int userId, int bankId, double initialInvestment)
        {
            int shortTermDepositId = -1;
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateCreateShortTermDeposit(userId, bankId, initialInvestment);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                shortTermDepositId = _bankService.createShortTermDeposit(userId, bankId, initialInvestment);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message, shortTermDepositId:shortTermDepositId);
        }
        public AppServiceUIMessage createLongTermDeposit(int userId, int bankId, int shortTermDepositId, int years, double initialInvestment)
        {
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateCreateLongTermDeposit(userId, bankId, shortTermDepositId, years, initialInvestment);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                _bankService.createLongTermDeposit(userId, bankId, shortTermDepositId, years, initialInvestment);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message);
        }
        public AppServiceUIMessage createGharzolhasaneDeposit(int userId, int bankId, double initialInvestment)
        {
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateCreateGharzolhasaneDeposit(userId, bankId, initialInvestment);
            if(validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                _bankService.createGharzolhasaneDeposit(userId, bankId, initialInvestment);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message);
        }
        public AppServiceUIMessage passTime(int months)
        {
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validatePassTime(months);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                foreach (Bank bank in BankRepository.Instance.BankList)
                {
                    _bankService.calculateAndDepositInterestForGivenMonths(bank.Id, months);
                }
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message);
        }
        public AppServiceUIMessage inventoryReport(int userId, int bankId, int shortTermDepositId)
        {
            double balance = 0.0D;
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateInventoryReport(userId, bankId, shortTermDepositId);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                balance = _bankService.getUserBalanceInCertainShortTermDeposit(userId, bankId, shortTermDepositId);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message, balance);
        }
        public AppServiceUIMessage calcMoneyInBank(int userId, int bankId)
        {
            double balance = 0.0D;
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateCalcMoneyInBank(userId, bankId);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                balance = _bankService.getUserBalanceInCertainBank(userId, bankId);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message, balance);
        }
        public AppServiceUIMessage calcAllMoney(int userId)
        {
            double balance = 0.0D;
            ValidatorAppServiceMessage validatorAppServiceMessage = _requestValidator.validateCalcAllMoney(userId);
            if (validatorAppServiceMessage.Message == ValidatorAppServiceMessage.MISSION_SUCCESSFUL)
            {
                balance = _bankService.getUserTotalBalance(userId);
            }
            return new AppServiceUIMessage(validatorAppServiceMessage.Message, balance);
        }
    }
}
