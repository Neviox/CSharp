/*
Napisati program koji upisuje dva cjelobrojna broja i ispisuje rezultat dijeljenja ta dva 
broja.Rezultat treba ispisati u sljedećim formatima (Currency, Integer, Scientific,
Fixed-point, General, Number, Hexadecimal). 
Prilikom upisa nekog podatka sa tipkovnice, podatak se učitava kao tip string, a ako 
nam treba tip int moramo ga pretvoriti uz pomoć ugrađenih metoda. 
Pripaziti da se obrade sve iznimke

2. Zadatak
Napisati program koji sadrži dvije varijable, jednu tipa int, a drugu tipa long u koju će 
biti zapisana najveća moguća vrijednost za tip long. Varijablu tipa long treba
pridružiti varijabli tipa int, s tim da se obradi iznimka ako dođe do preljeva 
(overflow). 
Pomoć: vidjeti “checked” u MSDN 

3. Zadatak
Napisati program za bankare koji ima deklariran pobrojani tip podataka u kojem se 
nalaze vrste računa (Štednja , Tekući račun, Žiro račun). Unutar programa deklarirati 
strukturu BankAccount koja će sadržavati tri varijable, broj računa, iznos na računu i 
vrstu računa. 
Program treba deklarirati polje struktura BankAccount od 5 elemenata, te napraviti 
izbornik koji će imati opcije, upisa novog računa, i ispis svih računa. Za ispis svih 
računa koristiti “foreach” iteraciju. 

*/

using System;

namespace Vjezba1
{
    class Program
    {
        private static void Main(string[] args)
        {
            ConvertInteger();
            CheckOverflow();
        } 

            private static void ConvertInteger()
            {
                ReadIntegerFromConsole(out int num1);
                ReadIntegerFromConsole(out int num2);

                if (num1 != 0 && num2 != 0)
                {
                    int result = num1 / num2;
                    Console.WriteLine("General: " + result.ToString("G"));
                    Console.WriteLine("Number: " + result.ToString("N"));
                    Console.WriteLine("Integer: " + result);
                    Console.WriteLine("Fixed-Point: " + result.ToString("F"));
                    Console.WriteLine("Currency: " + result.ToString("C"));
                    Console.WriteLine("Scientific: " + string.Format("{0:E2}", result));
                    Console.WriteLine("Hexadecimal: " + result.ToString("X"));
                }
            }
            private static void ReadIntegerFromConsole(out int number)
            {
                Console.WriteLine("Enter a number:");

                string numberFromConsole = Console.ReadLine();

                if (!int.TryParse(numberFromConsole, out number))
                {
                    Console.WriteLine("Wrong input value");
                    ReadIntegerFromConsole(out number);
                }
            }


            private static void CheckOverflow()
            {

                var longValue = long.MaxValue;
                int intValue;

                try
                {
                    // checks if there is a overflow and in that case, throws an exception 
                    checked
                    {
                        intValue = (int)longValue;
                    }

                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }


 
