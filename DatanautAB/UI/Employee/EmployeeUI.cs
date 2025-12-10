using DatanautAB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;



namespace DatanautAB.UI.Employee
{
    public static class EmployeeUI
    {
        public static void Show(DatanautContext context)
        {
            var repo = new DatanautRepository(context);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("==============================");
                Console.WriteLine("       Employee Meny");
                Console.WriteLine("==============================");
                Console.WriteLine("[1] Logga tid");
                Console.WriteLine("[2] Visa personlig rapport");
                Console.WriteLine("[0] Avsluta");
                Console.WriteLine("==============================");
                Console.Write("Välj alternativ: ");

                var employeeMenuChoice = Console.ReadLine();

                try
                {
                    switch (employeeMenuChoice)
                    {
                        case "1": EmployeeActions.LogTime(repo); break;
                        case "2": EmployeeActions.ShowPersonalReport(repo); break;
                        case "0": running = false; break;
                        default:
                            Console.WriteLine("Felaktigt val.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}
