using DatanautAB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.UI.ProjectManager
{
    public static class ProjectManagerUI
    {
        public static void Show(DatanautContext context)
        {
            var repo = new DatanautRepository(context);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("==============================");
                Console.WriteLine("       Project Manager Meny");
                Console.WriteLine("==============================");
                Console.WriteLine("[1] Skapa projekt");
                Console.WriteLine("[2] Redigera projekt");
                Console.WriteLine("[3] Visa projekt");
                Console.WriteLine("[4] Tilldela teammedlemmar");
                Console.WriteLine("[5] Hantera resurser");
                Console.WriteLine("[6] Projekt rapport");
                Console.WriteLine("[0] Avsluta");
                Console.WriteLine("==============================");
                Console.Write("Välj alternativ: ");

                var projectmanagerMenuChoice = Console.ReadLine();

                try
                {
                    switch (projectmanagerMenuChoice)
                    {
                        case "1": ProjectManagerActions.AddProject(repo); break;
                        case "2": ProjectManagerActions.UpdateProject(repo); break;
                        case "3": ProjectManagerActions.ViewProjects(repo); break;
                        case "4": ProjectManagerActions.AssignTeamMembers(repo); break;
                        case "5": ProjectManagerActions.ManageResources(repo); break;
                        case "6": ProjectManagerActions.GenerateProjectReport(repo); break;
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
