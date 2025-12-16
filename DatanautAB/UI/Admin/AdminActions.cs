using DatanautAB.Models;
using DatanautAB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.UI.Admin
{
    public static class AdminActions
    {
        // Lägg till teammedlem
        public static void AddTeamMember(DatanautRepository repo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Lägg till teammedlem ===");

                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine()!.Trim();
                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine()!.Trim();
                Console.Write("Email: ");
                string email = Console.ReadLine()!.Trim();

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Alla fält måste fyllas i!");
                    Console.ReadKey();
                    return;
                }

                // Välj roll
                var roles = repo.GetAllMemberRoles();
                if (!TrySelectFromList(roles.Select(r => r.RoleName).ToList(), out int roleIndex)) return;

                // Välj Skill
                var skills = repo.GetAllSkills();
                if (!TrySelectFromList(skills.Select(s => s.SkillName).ToList(), out int skillIndex)) return;

                var member = new TeamMember
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    FKMemberRoleID = roles[roleIndex].RoleID,
                    FKSkillID = skills[skillIndex].SkillID
                };

                repo.AddTeamMember(member);
                Console.WriteLine("Teammedlem tillagd!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid tillägg av teammedlem: {ex.Message}");
            }
            Console.ReadKey();
        }

        // Lista alla teammedlemmar
        public static void ListAllTeamMembers(DatanautRepository repo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Alla teammedlemmar ===");

                var members = repo.GetAllTeamMembers();
                foreach (var m in members)
                {
                    Console.WriteLine($"{m.TeamMemberID}: {m.FirstName} {m.LastName}, " +
                                      $"Roll: {m.FKMemberRole.RoleName}, Skill: {m.FKSkill.SkillName}");
                }
                if (!members.Any())
                    Console.WriteLine("Inga teammedlemmar registrerade!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid hämtning av teammedlemmar: {ex.Message}");
            }
            Console.ReadKey();
        }

        // Generera projektrapport
        public static void GenerateProjectReports(DatanautRepository repo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Projekt rapport ===");

                var reports = repo.GetProjectReports();
                foreach (var r in reports)
                {
                    Console.WriteLine($"ProjektID: {r.ProjectId}, Totalt timmar: {r.TotalHours}, Budget: {r.TotalBudget}");
                }
                if (!reports.Any())
                    Console.WriteLine("Inga rapporter tillgängliga!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid generering av projekt rapport: {ex.Message}");
            }
            Console.ReadKey();
        }

        // Generera medarbetarrapport
        public static void GenerateEmployeeReports(DatanautRepository repo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Medarbetar rapport ===");

                var reports = repo.GetEmployeeReports();
                foreach (var r in reports)
                {
                    Console.WriteLine($"TeamMemberID: {r.TeamMemberId}");
                    foreach (var h in r.HoursPerProject)
                    {
                        Console.WriteLine($"\tProjektID: {h.ProjectId}, Timmar: {h.Hours}");
                    }
                }
                if (!reports.Any())
                    Console.WriteLine("Inga rapporter tillgängliga!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid generering av medarbetar rapport: {ex.Message}");
            }
            Console.ReadKey();
        }

        // Hjälpfunktion för att säkerställa giltiga val när admin väljer roll eller skill
        private static bool TrySelectFromList(System.Collections.Generic.List<string> items, out int selectedIndex)
        {
            selectedIndex = -1;
            Console.WriteLine("Välj alternativ:");
            for (int i = 0; i < items.Count; i++)
                Console.WriteLine($"[{i}] {items[i]}");

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out selectedIndex) || selectedIndex < 0 || selectedIndex >= items.Count)
            {
                Console.WriteLine("Felaktigt val!");
                Console.ReadKey();
                return false;
            }
            return true;
        }
    }
}
