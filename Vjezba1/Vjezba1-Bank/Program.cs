using System;
using System.Collections.Generic;
using System.Globalization;


/*3. Zadatak
Napisati program za bankare koji ima deklariran pobrojani tip podataka u kojem se 
nalaze vrste računa (Štednja , Tekući račun, Žiro račun). Unutar programa deklarirati 
strukturu BankAccount koja će sadržavati tri varijable, broj računa, iznos na računu i 
vrstu računa. 
Program treba deklarirati polje struktura BankAccount od 5 elemenata, te napraviti 
izbornik koji će imati opcije, upisa novog računa, i ispis svih računa. Za ispis svih 
računa koristiti “foreach” iteraciju. */

namespace Vjezba1
{
    public struct BankAccount
    {
        public string number;
        public float amount;
        public AccountTypes accountType;
    }

    public enum AccountTypes
    {
        Savings = 0,
        Current_Account = 1,
        Giro_Account = 2
    }

    class Program
    {
        public const int MaxAccountNumbers = 5;

        static void Main(string[] args)
        {

            var bankAccounts = new BankAccount[MaxAccountNumbers];

            int numberOfAccounts = 0;

            while (numberOfAccounts <= MaxAccountNumbers)
            {
                Console.WriteLine("Enter 1 to register a new account, 2 to list all accounts and Exit to leave!");
                string userInput = Console.ReadLine().ToLower();

                if (userInput.Equals("1"))
                {
                    EnterAccount(bankAccounts, numberOfAccounts);
                    numberOfAccounts++;
                }
                else if (userInput.Equals("2"))
                {
                    PrintAccounts(bankAccounts);
                }
                else if (userInput.Equals("exit"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong input entered!");
                }
            }
        }

        private static void EnterAccount(BankAccount[] bankAccounts, int accountIndex)
        {
            Console.WriteLine("Enter the account number:");
            bankAccounts[accountIndex].number = Console.ReadLine();

            Console.WriteLine("Enter the account amount:");
            string amountInput = Console.ReadLine();
            bankAccounts[accountIndex].amount = float.Parse(amountInput, CultureInfo.InvariantCulture);

            Console.WriteLine("Enter the account type:");
            Console.WriteLine("(0 for Savings, 1 for Current account and 2 for Giro account)");
            ReadIntegerFromConsole(out int accountTypeNumber, new List<int> { 0, 1, 2 });
            bankAccounts[accountIndex].accountType = (AccountTypes)accountTypeNumber;
        }

        private static void PrintAccounts(BankAccount[] bankAccounts)
        {
            foreach (var bankAccount in bankAccounts)
            {
                if (!string.IsNullOrEmpty(bankAccount.number))
                    Console.WriteLine("Account number: " + bankAccount.number + " Account amount: " + bankAccount.amount + "$  Account type: " + bankAccount.accountType.ToString().Replace("_", " "));
            }
        }

        private static void ReadIntegerFromConsole(out int number, List<int> acceptedValues)
        {
            string numberFromConsole = Console.ReadLine();

            if (int.TryParse(numberFromConsole, out number) && acceptedValues.Contains(number))
                return;

            Console.WriteLine("Wrong input value");
            ReadIntegerFromConsole(out number, acceptedValues);
        }

    }
}