using DatanautAB.Data;
using DatanautAB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.UI.Admin
{
    public static class AdminUI
    {
        public static void Show(DatanautContext context)
        {
            var repo = new DatanautRepository(context);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("==============================");
                Console.WriteLine("       Admin Meny");
                Console.WriteLine("==============================");
                Console.WriteLine("[1] Lägg till teammedlem");
                Console.WriteLine("[2] Redigera teammedlem");
                Console.WriteLine("[3] Ta bort teammedlem");
                Console.WriteLine("[4] Generera periodrapport");
                Console.WriteLine("[0] Avsluta");
                Console.WriteLine("==============================");
                Console.Write("Välj alternativ: ");

                var adminMenuChoice = Console.ReadLine();

                try
                {
                    switch (adminMenuChoice) // UI läser input och skickar till actions, som validerar input och anropar repo, som sparar och hämtar data från databasen via dbcontext
                    {
                        case "1": AdminActions.AddTeamMember(repo); break;
                        case "2": AdminActions.UpdateTeamTember(repo); break;
                        case "3": AdminActions.DeleteTeamMember(repo); break;
                        case "4": AdminActions.GeneratePeriodReport(repo); break;
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
