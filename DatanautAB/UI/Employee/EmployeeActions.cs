using DatanautAB.Data;
using DatanautAB.Models;
using DatanautAB.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.UI.Employee
{
    public class EmployeeActions
    {
        public static void LogTime(DatanautRepository repo)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Rapportera tid ===");

                // Lists for selection
                var projectList = repo.GetAllProjects();
                var teamMemberList = repo.GetAllTeamMembers();
                var activityList = repo.GetAllActivities();

                if (projectList.Count() <= 0)
                {
                    Console.WriteLine("Kan ej rapportera tid. Projekt saknas i databasen.");
                    Console.ReadKey();
                    return;
                }
                else if (teamMemberList.Count() <= 0)
                {
                    Console.WriteLine("Kan ej rapportera tid. Lagmedlemmar saknas i databasen.");
                    Console.ReadKey();
                    return;
                }
                else if (activityList.Count() <= 0)
                {
                    Console.WriteLine("Kan ej rapportera tid. Aktivitetsinformation saknas i databasen.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("_____________________________");

                // Input requests
                foreach (var project in projectList)
                {
                    Console.WriteLine($"[ID: {project.ProjectID}| PROJEKTNAMN: {project.ProjectName}]");
                }
                Console.Write("\nSkriv in projekts-ID: ");
                string inProjectID = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(inProjectID))
                {
                    Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                    Console.ReadKey();
                    return;
                }
                if (!int.TryParse(inProjectID, out int validProjectID))
                {
                    Console.WriteLine("Ogiltigt inmatning. Ett heltal krävs.");
                    Console.ReadKey();
                    return;
                }
                if (repo.GetProjectById(validProjectID) == null)
                {
                    Console.WriteLine($"Ogiltigt inmatning. Projektetmed ID:{validProjectID} finns inte i databasen.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("_____________________________");

                foreach (var teamMember in teamMemberList)
                {
                    Console.WriteLine($"[ID: {teamMember.TeamMemberID}| NAMN: {teamMember.FirstName} {teamMember.LastName}]");
                }
                Console.Write("\nSkriv in Lagmedlems-ID: ");
                string inTeamMemberID = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(inTeamMemberID))
                {
                    Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                    Console.ReadKey();
                    return;
                }
                if (!int.TryParse(inTeamMemberID, out int validTeamMemberID))
                {
                    Console.WriteLine("Ogiltigt inmatning. Ett heltal krävs.");
                    Console.ReadKey();
                    return;
                }
                if (repo.GetTeamMemberById(validTeamMemberID) == null)
                {
                    Console.WriteLine($"Ogiltigt inmatning. Lagmedlem med ID:{validTeamMemberID} finns inte i databasen.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("_____________________________");

                foreach (var activity in activityList)
                {
                    Console.WriteLine($"[ID: {activity.ActivityID}| AKTIVITET: {activity.ActivityName}]");
                }
                Console.Write("\nSkriv in Aktivitets-ID: ");
                string inActivityID = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(inActivityID))
                {
                    Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                    Console.ReadKey();
                    return;
                }
                if (!int.TryParse(inActivityID, out int validActivityID))
                {
                    Console.WriteLine("Ogiltigt inmatning. Ett heltal krävs.");
                    Console.ReadKey();
                    return;
                }
                if (repo.GetTeamMemberById(validTeamMemberID) == null)
                {
                    Console.WriteLine($"Ogiltigt inmatning. Aktivitet med ID:{validTeamMemberID} finns inte i databasen.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("_____________________________");

                Console.Write("\nHur mycket tid spenderades (HH:MM): ");
                string inTimeSpent = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(inTimeSpent))
                {
                    Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                    Console.ReadKey();
                    return;
                }
                if (!TimeSpan.TryParse(inTimeSpent, out TimeSpan validTimeSpent))
                {
                    Console.WriteLine("Ogiltigt inmatning. Rätt formatet krävs.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("_____________________________");

                Console.Write("\nDatum för rapport (YYYY-MM-DD): ");
                string inLogDate = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(inLogDate))
                {
                    Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                    Console.ReadKey();
                    return;
                }
                if (!DateTime.TryParse(inLogDate, out DateTime validLogDate))
                {
                    Console.WriteLine("Ogiltigt inmatning. Rätt formatet krävs.");
                    Console.ReadKey();
                    return;
                }

                var timeLog = new TimeLog
                {
                    FKProjectID = validProjectID,
                    FKTeamMemberID = validTeamMemberID,
                    LogDate = validLogDate,
                    TimeSpent = validTimeSpent,
                    FKActivity = repo.GetAllActivities().FirstOrDefault(a => a.ActivityID == validActivityID),
                    FKProject = repo.GetProjectById(validProjectID),
                    FKTeamMember = repo.GetAllTeamMembers().FirstOrDefault(tm => tm.TeamMemberID == validTeamMemberID)
                };

                repo.AddTimeLog(timeLog);
                Console.WriteLine("Tidsrapporten tillagd!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid tillägg av tidsrapport: {ex.Message}");
            }
            Console.ReadKey();
        }

        public static void ShowPersonalReport(DatanautRepository repo)
        {
            var timeLogs = repo.GetAllTimeLogs();
            if(timeLogs.Count() < 0)
            {
                Console.WriteLine("Tidsrapporter saknas i databasen.");
                Console.ReadKey();
                return;
            }

            Console.Write("\nSkriv in ditt lagmedlems-ID: ");
            string inTeamMemberID = Console.ReadLine()!.Trim();
            if (string.IsNullOrEmpty(inTeamMemberID))
            {
                Console.WriteLine("Inmatning kan ej vara tom. Försök igen!");
                Console.ReadKey();
                return;
            }
            if (!int.TryParse(inTeamMemberID, out int validTeamMemberID))
            {
                Console.WriteLine("Ogiltigt inmatning. Ett heltal krävs.");
                Console.ReadKey();
                return;
            }
            if (repo.GetTeamMemberById(validTeamMemberID) == null)
            {
                Console.WriteLine($"Ogiltigt inmatning. Lagmedlem med ID:{validTeamMemberID} finns inte i databasen.");
                Console.ReadKey();
                return;
            }

            var currentTeamMember = repo.GetTeamMemberById(validTeamMemberID);
            var foundTimeLogs = timeLogs
                .Where(tl => tl.FKTeamMemberID == validTeamMemberID)
                .ToList();

            Console.WriteLine($"[ID: {currentTeamMember.TeamMemberID} | NAMN: {currentTeamMember.FirstName} {currentTeamMember.LastName}]");
            foreach (var foundTimeLog in foundTimeLogs)
            {
                // Note: String format here is very specific
                Console.WriteLine($@"
  PROJEKT: {foundTimeLog.FKProject.ProjectName}
  DATUM: {foundTimeLog.LogDate}
  TID: {foundTimeLog.TimeSpent.ToString(@"hh\:mm")}
  ACTIVITET: {repo.GetAllActivities().FirstOrDefault(a => a.ActivityID == foundTimeLog.FKActivityID).ActivityName}
");
            }
            Console.ReadKey();
        }
    }
}
