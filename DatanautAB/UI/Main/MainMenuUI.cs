using DatanautAB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.UI.MainMenu
{
    public static class MainMenuUI
    {
        public static void Show(DatanautContext context)
           
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==============================");
                Console.WriteLine("       Huvudmeny");
                Console.WriteLine("==============================");
                Console.WriteLine("[1] Admin");
                Console.WriteLine("[2] Project Manager");
                Console.WriteLine("[3] Employee");
                Console.WriteLine("[0] Avsluta");
                Console.WriteLine("==============================");
                Console.Write("Välj alternativ: ");

                var mainMenuChoice = Console.ReadLine();

                try
                {
                    switch (mainMenuChoice) 
                    {
                        case "1": UI.Admin.AdminUI.Show(context); break;
                        case "2": UI.ProjectManager.ProjectManagerUI.Show(context); break;
                        case "3": UI.Employee.EmployeeUI.Show(context); break;
                        case "0": running = false; return;
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
