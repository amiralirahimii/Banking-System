using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.DTO;

namespace BankingSystem.DataAccess.Repository
{
    internal class DepositRepository
    {
        private static readonly DepositRepository _instance = new DepositRepository();
        private static int _lastDepsoitId = 0;
        private DepositRepository()
        {
            _depositList = new List<Deposit>();
        }
        public static DepositRepository Instance
        {
            get
            {
                return _instance;
            }
        }
        private List<Deposit> _depositList;
        public IEnumerable<Deposit> DepositList
        {
            get
            {
                foreach (Deposit deposit in _depositList)
                {
                    yield return deposit.copy();
                }
            }
        }
        public bool doesDepositExists(int depositId)
        {
            return _depositList.Any(deposit => depositId == deposit.DepositId);
        }
        public Deposit getDepositById(int depositId)
        {
            Deposit targetDeposit = _depositList.Single(deposit => depositId == deposit.DepositId);
            return targetDeposit.copy();
        }
        public void updateDeposit(Deposit updatedDeposit)
        {
            int index = _depositList.FindIndex(deposit => updatedDeposit.DepositId == deposit.DepositId);
            _depositList[index] = updatedDeposit;
        }
        public void AddDeposit(Deposit deposit)
        {
            _depositList.Add(deposit);
        }
        public int GetNextDepositId()
        {
            return _lastDepsoitId++;
        }
    }
}









