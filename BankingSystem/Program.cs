using BankingSystem.DataAccess;
using BankingSystem.UI;

namespace BankingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new DataLoader(args[0], args[1]).Load();
            UserInterface userInterface = new UserInterface();
            userInterface.startInteractingWithUser("1.in");
        }
    }
}
