using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DataAccess.DTO
{
    internal class Bank
    {
        public Bank(int id, int shortTermProfitMargin, double shortTermMinimumInvestemnt)
        {
            Id = id;
            ShortTermProfitMargin = shortTermProfitMargin;
            ShortTermMinimumInvestemnt = shortTermMinimumInvestemnt;
            _lastShortTermDepositId = 1;
        }
        public int Id { get; set; }
        public int ShortTermProfitMargin { get; set; }
        public double ShortTermMinimumInvestemnt { get; set; }
        private int _lastShortTermDepositId;
        public List<int> DepositList { get; set; }
        public Bank copy()
        {
            Bank bankCopy = new Bank(Id, ShortTermProfitMargin, ShortTermMinimumInvestemnt);
            bankCopy._lastShortTermDepositId = _lastShortTermDepositId;
            if (DepositList != null)
                bankCopy.DepositList = new List<int> (DepositList);
            else
                bankCopy.DepositList = new List<int> ();
                return bankCopy;
        }
        public void AddDepositId(int depositId)
        {
            DepositList.Add(depositId);
        }
        public int GetNextShortTermDepositId()
        {
            return _lastShortTermDepositId++;
        }
    }
}
