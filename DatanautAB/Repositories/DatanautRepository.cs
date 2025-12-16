using DatanautAB.Data;
using DatanautAB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatanautAB.Repositories
{
    public class DatanautRepository
    {
        private readonly DatanautContext _context;
        public DatanautRepository(DatanautContext context)
        {
            _context = context;
        }

        // Projekt
        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void DeleteProject(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null) return;

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
        public Project GetProjectById(int projectId)
        {
            return _context.Projects
                .Include(p => p.FKProjectManager)
                .Include(p => p.FKProjectStatus)
                .Include(p => p.ProjectTeams)
                .ThenInclude(pt => pt.FKTeamMember)
                .Include(p => p.ProjectResources)
                .ThenInclude(pr => pr.FKEquipment)
                .Include(p => p.ProjectResources)
                .ThenInclude(pr => pr.FKLicense)
                .Include(p => p.ProjectResources)
                .ThenInclude(pr => pr.FKSoftware)
                .Include(p => p.TimeLogs)
                .ThenInclude(t => t.FKActivity)
                .FirstOrDefault(p => p.ProjectID == projectId);
        }
        public List<Project> GetAllProjects()
        {
            return _context.Projects
                .Include(p => p.FKProjectManager)
                .Include(p => p.FKProjectStatus)
                .Include(p => p.ProjectTeams)
                    .ThenInclude(pt => pt.FKTeamMember)
                .Include(p => p.ProjectResources)
                    .ThenInclude(pr => pr.FKEquipment)
                .Include(p => p.ProjectResources)
                    .ThenInclude(pr => pr.FKLicense)
                .Include(p => p.ProjectResources)
                    .ThenInclude(pr => pr.FKSoftware)
                .ToList();
        }

        // Projekt team
        public void AssignTeamMemberToProject(int projectId, int teamMemberId)
        {
            if (!_context.Projects.Any(p => p.ProjectID == projectId))
                throw new InvalidOperationException("Projekt finns inte!");

            if (!_context.TeamMembers.Any(t => t.TeamMemberID == teamMemberId))
                throw new InvalidOperationException("Teammedlem finns inte!");

            var exists = _context.ProjectTeams.Any(pt => pt.FKProjectID == projectId && pt.FKTeamMemberID == teamMemberId);
            if (exists) return;

            var ptEntry = new ProjectTeam
            {
                FKProjectID = projectId,
                FKTeamMemberID = teamMemberId
            };

            try
            {
                _context.ProjectTeams.Add(ptEntry);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Tidslogg med transaktion
        public void LogTime(int projectId, int memberId, int activityId, TimeSpan time)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var log = new TimeLog
                {
                    FKProjectID = projectId,
                    FKTeamMemberID = memberId,
                    FKActivityID = activityId,
                    LogDate = DateTime.Now,
                    TimeSpent = time
                };

                _context.TimeLogs.Add(log);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        // TeamMembers
        public void AddTeamMember(TeamMember member)
        {
            _context.TeamMembers.Add(member);
            _context.SaveChanges();
        }

        public void UpdateTeamMember(TeamMember member)
        {
            _context.TeamMembers.Update(member);
            _context.SaveChanges();
        }

        public void DeleteTeamMember(int memberId)
        {
            var member = _context.TeamMembers.Find(memberId);
            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                _context.SaveChanges();
            }
        }
        public TeamMember GetTeamMemberById(int memberId)
        {
            return _context.TeamMembers
                .Include(t => t.FKMemberRole)
                .Include(t => t.FKSkill)
                .Include(t => t.Projects)
                .Include(t => t.TimeLogs)
                .FirstOrDefault(t => t.TeamMemberID == memberId);
        }

        public List<TeamMember> GetAllTeamMembers()
        {
            return _context.TeamMembers
                .Include(t => t.FKMemberRole)
                .Include(t => t.FKSkill)
                .ToList();
        }

        // Roles
        public List<MemberRole> GetAllMemberRoles()
        {
            return _context.MemberRoles.ToList();
        }

        // Skills
        public List<Skill> GetAllSkills()
        {
            return _context.Skills.ToList();
        }

        // Activity
        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        // Reports
        public List<ProjectReport> GetProjectReports()
        {
            return _context.TimeLogs
                .GroupBy(t => t.FKProjectID)
                .Select(g => new ProjectReport
                {
                    ProjectId = g.Key,
                    TotalHours = g.Sum(t => t.TimeSpent.TotalHours),
                    TotalBudget = _context.Projects
                        .Where(p => p.ProjectID == g.Key)
                        .Select(p => p.Budget ?? 0)
                        .FirstOrDefault()
                }).ToList();
        }

        public List<EmployeeReport> GetEmployeeReports()
        {
            return _context.TimeLogs
                .GroupBy(t => t.FKTeamMemberID)
                .Select(g => new EmployeeReport
                {
                    TeamMemberId = g.Key,
                    HoursPerProject = g.GroupBy(t => t.FKProjectID)
                                       .Select(p => new HoursPerProject
                                       {
                                           ProjectId = p.Key,
                                           Hours = (decimal)p.Sum(x => x.TimeSpent.TotalHours)
                                       }).ToList()
                }).ToList();
        }

        public List<ProjectReport> GetPeriodReports(DateTime start, DateTime end)
        {
            return _context.TimeLogs
                .Where(t => t.LogDate >= start && t.LogDate <= end)
                .GroupBy(t => t.FKProjectID)
                .Select(g => new ProjectReport
                {
                    ProjectId = g.Key,
                    TotalHours = g.Sum(t => t.TimeSpent.TotalHours),
                    TotalBudget = _context.Projects
                        .Where(p => p.ProjectID == g.Key)
                        .Select(p => p.Budget ?? 0)
                        .FirstOrDefault()
                }).ToList();
        }

        internal void AddTimeLog(TimeLog timeLog)
        {
            _context.TimeLogs.Add(timeLog);
            _context.SaveChanges();
        }

        public List<TimeLog> GetAllTimeLogs()
        {
            return _context.TimeLogs.ToList();
        }

        // Report classes
        public class ProjectReport
        {
            public int ProjectId { get; set; }
            public decimal TotalBudget { get; set; }
            public double TotalHours { get; set; }
        }

        public class EmployeeReport
        {
            public List<HoursPerProject> HoursPerProject { get; set; } = new();
            public int TeamMemberId { get; set; }
        }

        public class HoursPerProject
        {
            public int ProjectId { get; set; }
            public decimal Hours { get; set; }
        }
        public class ProjectReportDetailed
        {
            public List<ProjectResource> Resources { get; set; } = new();
            public int ProjectId { get; set; }
            public decimal TotalBudget { get; set; }
            public double TotalHours { get; set; }
        }


    }
}
