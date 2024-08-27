using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.DTO;

namespace BankingSystem.DataAccess.Repository
{
    internal class BankRepository
    {
        private static readonly BankRepository _instance = new BankRepository();
        private BankRepository()
        {
            _bankList = new List<Bank>();
        }
        public static BankRepository Instance
        {
            get
            {
                return _instance;
            }
        }
        public List<Bank> _bankList;
        public IEnumerable<Bank> BankList
        {
            get
            {
                foreach (Bank bank in _bankList)
                {
                    yield return bank.copy();
                }
            }
        }
        public bool doesBankExists(int bankId)
        {
            return _bankList.Any(bank => bankId == bank.Id);
        }
        public Bank getBankById(int bankId)
        {
            Bank targetBank = _bankList.Single(bank => bank.Id == bankId);
            return targetBank.copy();
        }
        public void updateBank(Bank updatedBank) // Check if it is a correct way to update the value
        {
            int index = _bankList.FindIndex(bank => bank.Id == updatedBank.Id);
            _bankList[index] = updatedBank;
        }
        public void AddBank(Bank bank)
        {
            _bankList.Add(bank);
        }
    }

}