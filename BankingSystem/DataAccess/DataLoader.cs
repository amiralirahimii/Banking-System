using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.DataAccess.DTO;
using BankingSystem.DataAccess.Repository;

namespace BankingSystem.DataAccess
{
    internal class DataLoader
    {
        public DataLoader(string bankCsvPath, string userCsvPath)
        {
            BankCsvPath = bankCsvPath;
            UserCsvPath = userCsvPath;
        }
        public string BankCsvPath { get; set; }
        public string UserCsvPath { get; set; }
        private void LoadBanks()
        {
            using (var reader = new StreamReader(BankCsvPath))
            {
                var throwAway = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Bank newBank = new Bank(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
                    BankRepository.Instance.AddBank(newBank);
                }
            }
        }
        private void LoadUsers()
        {
            using (var reader = new StreamReader(UserCsvPath))
            {
                var throwAway = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    User newUser = new User(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]));
                    UserRepository.Instance.AddUser(newUser);
                }
            }
        }
        public void Load()
        {
            LoadBanks();
            LoadUsers();
        }
    }
}
