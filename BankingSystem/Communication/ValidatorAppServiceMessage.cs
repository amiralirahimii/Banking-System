using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Communication
{
    internal class ValidatorAppServiceMessage
    {
        public static readonly string NOT_ENOUGH_MONEY = "Not enough money";
        public static readonly string INVALID_SHORT_TERM_DEPOSIT = "Invalid short-term deposit";
        public static readonly string MISSION_SUCCESSFUL = "OK";
        public ValidatorAppServiceMessage(string message, int shortTermDepositId = -1)
        {
            Message = message;
            ShortTermDepositId = shortTermDepositId;
        }
        public string Message {  get; set; }
        public int ShortTermDepositId { get; set; }
    }
}
