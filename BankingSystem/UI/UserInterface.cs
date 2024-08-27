using BankingSystem.Communication;
using BankingSystem.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.UI
{
    internal class UserInterface
    {
        public static readonly string CREATE_SHORT_TERM_DEPOSIT = "create_short_term_deposit";
        public static readonly string CREATE_LONG_TERM_DEPOSIT = "create_long_term_deposit";
        public static readonly string CREATE_GHARZOLHASANE_DEPOSIT = "create_gharzolhasane_deposit";
        public static readonly string PASS_TIME = "pass_time";
        public static readonly string INVENTORY_REPORT = "inventory_report";
        public static readonly string CALC_MONEY_IN_BANK = "calc_money_in_bank";
        public static readonly string CALC_ALL_MONEY = "calc_all_money";
        public static readonly string INVALID_REQUEST = "Invalid request brother";
        private static readonly Service.ApplicationService _applicationService = new Service.ApplicationService();
        public UserInterface(){}
        public void startInteractingWithUser(string commandsFilePath)
        {
            var lines = File.ReadAllLines(commandsFilePath);
            foreach (var line in lines)
            {
                string[] requestArray = line.Split(' ');

                if (requestArray.Length == 0)
                {
                    Console.WriteLine(INVALID_REQUEST);
                    continue;
                }

                if (requestArray[0] == CREATE_SHORT_TERM_DEPOSIT)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    int bankId = Convert.ToInt32(requestArray[2]);
                    double initialInvestment = Convert.ToDouble(requestArray[3]);
                    AppServiceUIMessage appServiceUIMessage = _applicationService.createShortTermDeposit(userId, bankId, initialInvestment);
                    Console.WriteLine(appServiceUIMessage.Message == AppServiceUIMessage.MISSION_SUCCESSFUL ? appServiceUIMessage.ShortTermDepositId : appServiceUIMessage.Message);
                }
                else if (requestArray[0] == CREATE_LONG_TERM_DEPOSIT)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    int bankId = Convert.ToInt32(requestArray[2]);
                    int shortTermDepositId = Convert.ToInt32(requestArray[3]);
                    int years = Convert.ToInt32(requestArray[4]);
                    double initialInvestment = Convert.ToDouble(requestArray[5]);
                    Console.WriteLine(_applicationService.createLongTermDeposit(userId, bankId, shortTermDepositId, years, initialInvestment).Message);
                }
                else if (requestArray[0] == CREATE_GHARZOLHASANE_DEPOSIT)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    int bankId = Convert.ToInt32(requestArray[2]);
                    double initialInvestment = Convert.ToDouble(requestArray[3]);
                    Console.WriteLine(_applicationService.createGharzolhasaneDeposit(userId, bankId, initialInvestment).Message);
                }
                else if (requestArray[0] == PASS_TIME)
                {
                    int months = Convert.ToInt32(requestArray[1]);
                    Console.WriteLine(_applicationService.passTime(months).Message);
                }
                else if (requestArray[0] == INVENTORY_REPORT)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    int bankId = Convert.ToInt32(requestArray[2]);
                    int shortTermDepositId = Convert.ToInt32(requestArray[3]);
                    AppServiceUIMessage appServiceUIMessage = _applicationService.inventoryReport(userId, bankId, shortTermDepositId);
                    Console.WriteLine(appServiceUIMessage.Message == AppServiceUIMessage.MISSION_SUCCESSFUL ? appServiceUIMessage.RequestedBalance.ToString("F2") : appServiceUIMessage.Message);
                }
                else if (requestArray[0] == CALC_MONEY_IN_BANK)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    int bankId = Convert.ToInt32(requestArray[2]);
                    AppServiceUIMessage appServiceUIMessage = _applicationService.calcMoneyInBank(userId, bankId);
                    Console.WriteLine(appServiceUIMessage.Message == AppServiceUIMessage.MISSION_SUCCESSFUL ? appServiceUIMessage.RequestedBalance.ToString("F2") : appServiceUIMessage.Message);
                }
                else if (requestArray[0] == CALC_ALL_MONEY)
                {
                    int userId = Convert.ToInt32(requestArray[1]);
                    AppServiceUIMessage appServiceUIMessage = _applicationService.calcAllMoney(userId);
                    Console.WriteLine(appServiceUIMessage.Message == AppServiceUIMessage.MISSION_SUCCESSFUL ? appServiceUIMessage.RequestedBalance.ToString("F2") : appServiceUIMessage.Message);
                }
                else
                {
                    Console.WriteLine(INVALID_REQUEST);
                }
            }
        }
    }
}
