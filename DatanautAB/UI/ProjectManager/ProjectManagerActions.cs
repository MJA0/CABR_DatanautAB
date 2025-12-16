using DatanautAB.Data;
﻿using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatanautAB.Models;
using DatanautAB.Repositories;

namespace DatanautAB.UI.ProjectManager
{
    public static class ProjectManagerActions
    {
        // Skapa nytt projekt
        public static void CreateProject(DatanautRepository repo)
        {
            Console.WriteLine("Projekt namn: ");
            var projectNameInput = Console.ReadLine();
            Console.WriteLine("Projekt start datum (yyyy-MM-dd): ");
            var startDateInput = Console.ReadLine();

            Console.WriteLine("Projekt slut datum (yyyy-MM-dd): ");
            var endDateInput = Console.ReadLine();

            Console.WriteLine("Projekt budget: ");
            var budgetInput = Console.ReadLine();

            Console.WriteLine("Projekt manager ID: ");
            var projectManagerIdInput = Console.ReadLine();

            if (string.IsNullOrEmpty(projectNameInput))
            {
                Console.WriteLine("Projekt namn kan inte vara tomt.");
                return;
            }

            if (!DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                Console.WriteLine("ogiltigt start datum.");
                return;
            }

            if (!DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                Console.WriteLine("ogiltigt slut datum.");
                return;
            }

            if (startDate >= endDate)
            {
                Console.WriteLine("start datum måste vara tidigare än slut datum.");
                return;
            }

            if (!decimal.TryParse(budgetInput, out decimal budget))
            {
                Console.WriteLine("ogiltigt budget format.");
                return;
            }

            if (!int.TryParse(projectManagerIdInput, out int projectManagerId))
            {
                Console.WriteLine("ogiltigt ID format.");
                return;
            }

            var project = new Project
            {
                ProjectName = projectNameInput,
                StartDate = DateOnly.FromDateTime(startDate),
                EndDate = DateOnly.FromDateTime(endDate),
                Budget = budget,
                FKProjectManagerID = projectManagerId
            };

            repo.AddProject(project);

            Console.WriteLine("projekt skapat!");
        }

        // redigera nuvarande projekt
        public static void EditProject(DatanautRepository repo)
        {
            // Implementation för att redigera projekt
            Console.WriteLine("Lägg till ID för vilket projekt du vill ändra: ");
            var projectIdInput = Console.ReadLine();
            if (!int.TryParse(projectIdInput, out int projectId))
            {
                Console.WriteLine("ogiltigt ID format.");
                return;
            }
            var project = repo.GetProjectById(projectId);

            if (project == null)
            {
                Console.WriteLine("Projekt ej hittat.");
                return;
            }
            Console.WriteLine($"Redigierar projekt: {project.ProjectName}");
            Console.WriteLine("nytt projekt namn (lämna tomt för att behålla föregående): ");
            var projectNameInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(projectNameInput))
            {
                project.ProjectName = projectNameInput;
            }
            Console.WriteLine("nytt projekt start datum(yyyy-MM-dd) (lämna tomt för att behålla föregående): ");
            var startDateInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(startDateInput))
            {
                if (DateTime.TryParse(startDateInput, out DateTime startDate))
                {
                    project.StartDate = DateOnly.FromDateTime(startDate);
                }
                else
                {
                    Console.WriteLine("ogiltigt start datum.");
                    return;
                }
            }
            Console.WriteLine("nytt projekt slut datum (yyyy-MM-dd) (lämna tomt för att behålla föregående): ");
            var endDateInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(endDateInput))
            {
                if (DateTime.TryParse(endDateInput, out DateTime endDate))
                {
                    project.EndDate = DateOnly.FromDateTime(endDate);
                }
                else
                {
                    Console.WriteLine("ogiltigt slut datum.");
                    return;
                }
            }
            Console.WriteLine("ny projekt budget (lämna tomt för att behålla föregående): ");
            var budgetInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(budgetInput))
            {
                if (decimal.TryParse(budgetInput, out decimal budget))
                {
                    project.Budget = budget;
                }
                else
                {
                    Console.WriteLine("ogiltigt budget format.");
                    return;
                }
            }
            repo.UpdateProject(project);
        }
        public static void ViewProjects(DatanautRepository repo)
        {
            // Implementation för att visa projekt
            var projects = repo.GetAllProjects();
            Console.WriteLine("projekt:");
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.ProjectID}, Namn: {project.ProjectName}, Start: {project.StartDate}, slut: {project.EndDate}, Budget: {project.Budget}, Manager ID: {project.FKProjectManagerID}");
            }
        }
        public static void AssignTeamMembers(DatanautRepository repo)
        {
            // metod för att tilldela member till projekt
            Console.WriteLine("välj ID för att tilldela medlem: ");
            var projectIdInput = Console.ReadLine();
            if (!int.TryParse(projectIdInput, out int projectId))
            {
                Console.WriteLine("ogoltigt ID format.");
                return;
            }
            foreach (var member in repo.GetAllTeamMembers())
            {
                Console.WriteLine($"ID: {member.TeamMemberID}, Namn: {member.FirstName} {member.LastName}");
            }
            Console.WriteLine("Välj medlems ID för att tilldela projekt: ");
            var teamMemberIdInput = Console.ReadLine();
            if (!int.TryParse(teamMemberIdInput, out int teamMemberId))
            {
                Console.WriteLine("ogiltigt ID.");
                return;
            }
            repo.AssignTeamMemberToProject(projectId, teamMemberId);
        }
        public static void HandleResources(DatanautRepository repo)
        {
            bool running = true;

            while (running)
            {
                // Visa aktuell resursinformation
                Console.WriteLine("nuvarande resurser:");
                var resources = repo.GetAllResources();

                if (resources.Count == 0)
                {
                    Console.WriteLine("inga resorces hittade.");
                }
                else
                {
                    foreach (var resource in resources)
                    {
                        Console.WriteLine($"ID: {resource.ResourceID}, Namn: {resource.FKLicense}, Typ: {resource.FKSoftware}, Pris: {resource.FKEquipmentID}");
                    }
                }

                // Visa meny för att hantera resurser
                Console.WriteLine("\\n==============================");
                Console.WriteLine("       Hantera Resurs");
                Console.WriteLine("==============================");
                Console.WriteLine("[1] Lägg till ny Lisens");
                Console.WriteLine("[2] Lägg till ny mjukvara");
                Console.WriteLine("[3] Lägg till utrustning");
                Console.WriteLine("[0] Back to Main Menu");
                Console.WriteLine("==============================");
                Console.Write("Välj: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddLicense(repo);
                        break;
                    case "2":
                        AddSoftware(repo);
                        break;
                    case "3":
                        AddEquipment(repo);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        private static void AddLicense(DatanautRepository repo)
        {
            Console.WriteLine("Välj lisens namn: ");
            var licenseName = Console.ReadLine();
            Console.WriteLine("Välj lisens pris: ");
            var costInput = Console.ReadLine();
            if (!decimal.TryParse(costInput, out decimal cost))
            {
                Console.WriteLine("ogiltigt format.");
                return;
            }
            var license = new License
            {
                LicenseName = licenseName,
                ExpirationDate = null,
                LicenseQuantity = 1,
                Cost = cost
            };
            repo.AddLicense(license);
            Console.WriteLine("Lisens tillagd!");
        }
        private static void AddSoftware(DatanautRepository repo)
        {
            Console.WriteLine("Välj mjukvara namn: ");
            var softwareName = Console.ReadLine();
            Console.WriteLine("Välj mjukvara version: ");
            var version = Console.ReadLine();
            var software = new Software
            {
                SoftwareName = softwareName,
                SoftwareVersion = version,
                SoftwareQuantity = 1

            };
            repo.AddSoftware(software);
            Console.WriteLine("Mjukvara tillagd");
        }
        private static void AddEquipment(DatanautRepository repo)
        {
            Console.WriteLine("Välj namn på utrustning: ");
            var equipmentName = Console.ReadLine();
            Console.WriteLine("Välj utrustningstyp: ");
            var type = Console.ReadLine();
            Console.WriteLine("Välj utrsustningspris: ");
            var costInput = Console.ReadLine();
            if (!decimal.TryParse(costInput, out decimal cost))
            {
                Console.WriteLine("ogiltigt format.");
                return;
            }
            var equipment = new Equipment
            {
                EquipmentName = equipmentName,
                EquipmentQuantity = 1,
                SerialNumber = type,
                Cost = cost
            };
            repo.AddEquipment(equipment);
            Console.WriteLine("Utrustning tillagd!");
        }
        // projekt rapport
        public static void GenerateProjectReport(DatanautRepository repo)
        {
            // genererar projekt rapport
            Console.WriteLine("Välj projekt ID för att se rapport: ");
            var projectIdInput = Console.ReadLine();
            if (!int.TryParse(projectIdInput, out int projectId))
            {
                Console.WriteLine("ogiltigt ID format");
                return;
            }
            var project = repo.GetProjectById(projectId);
            if (project == null)
            {
                Console.WriteLine("Projekt ej hittat");
                return;
            }
            Console.WriteLine($"Projekt Repport för: {project.ProjectName}");
            Console.WriteLine($"Start Datum: {project.StartDate}");
            Console.WriteLine($"Slut Datum: {project.EndDate}");
            Console.WriteLine($"Budget: {project.Budget}");
            Console.WriteLine("Team Medlemmar:");
            foreach (var teamMember in project.ProjectTeams)
            {
                Console.WriteLine($"- {teamMember.FKTeamMember.FirstName} {teamMember.FKTeamMember.LastName}");
            }
            Console.WriteLine("Resurser:");
            foreach (var resource in project.ProjectResources)
            {
                if (resource.FKLicense != null)
                {
                    Console.WriteLine($"- Lisens: {resource.FKLicense.LicenseName}");
                }
                if (resource.FKSoftware != null)
                {
                    Console.WriteLine($"- Mjukvara: {resource.FKSoftware.SoftwareName}");
                }
                if (resource.FKEquipment != null)
                {
                    Console.WriteLine($"- Utrustning: {resource.FKEquipment.EquipmentName}");
                }
            }
        }
    }
}
