using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Communication
{
    internal class AppServiceUIMessage
    {
        public static readonly string NOT_ENOUGH_MONEY = "Not enough money";
        public static readonly string INVALID_SHORT_TERM_DEPOSIT = "Invalid short-term deposit";
        public static readonly string MISSION_SUCCESSFUL = "OK";
        public AppServiceUIMessage(string message, double requestedBalance = 0.0D, int shortTermDepositId = -1)
        {
            Message = message;
            RequestedBalance = requestedBalance;
            ShortTermDepositId = shortTermDepositId;
        }
        public string Message { get; set; }
        public double RequestedBalance { get; set; }
        public int ShortTermDepositId { get; set; }
    }
}
