using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatanautAB.Data;
using DatanautAB.Models;
using Microsoft.IdentityModel.Tokens;

namespace DatanautAB.DataSeed
{
    public static class DbSeeder
    {
        public static void Seed(DatanautContext context)
        {
            context.Database.EnsureCreated();

            if (context.TeamMembers.Any()) return; // Körs en gång

            // Roles
            var adminRole = new MemberRole { RoleName = "Admin" };
            var projectManagerRole = new MemberRole { RoleName = "Project Manager" };
            var employeeRole = new MemberRole { RoleName = "Employee" };
            context.MemberRoles.AddRange(adminRole, projectManagerRole, employeeRole);

            // Skills
            var developerSkill = new Skill { SkillName = "Developer" };
            var qualityAssuranceSkill = new Skill { SkillName = "Quality Assurance" };
            var systemsEngineerSkill = new Skill { SkillName = "Systems Engineer" };
            context.Skills.AddRange(developerSkill, qualityAssuranceSkill, systemsEngineerSkill);

            // Project Status
            var completed = new ProjectStatus { StatusName = "Completed" };
            var notStarted = new ProjectStatus { StatusName = "Not Started" };
            var inProgress = new ProjectStatus { StatusName = "In Progress" };
            var onHold = new ProjectStatus { StatusName = "On Hold" };
            var cancelled = new ProjectStatus { StatusName = "Cancelled" };
            context.ProjectStatuses.AddRange(completed, notStarted, inProgress, onHold, cancelled);

            // Activities
            var developmentActivity = new Activity { ActivityName = "Development" };
            var testingActivity = new Activity { ActivityName = "Testing" };
            var documentationActivity = new Activity { ActivityName = "Documentation" };
            context.Activities.AddRange(developmentActivity, testingActivity, documentationActivity);

            context.SaveChanges(); // Viktigt för att spara FK-Ids

            // Team Members
            var robin = new TeamMember
            {
                FirstName = "Robin",
                LastName = "Markström",
                Email = "Robin.Markstrom@datanaut.com",
                FKMemberRoleID = adminRole.RoleID,
                FKSkillID = developerSkill.SkillID
            };

            var adcha = new TeamMember
            {
                FirstName = "Adchariya",
                LastName = "Changtam",
                Email = "Adchariya.Changtam@datanaut.com",
                FKMemberRoleID = projectManagerRole.RoleID,
                FKSkillID = systemsEngineerSkill.SkillID
            };

            var niklas = new TeamMember
            {
                FirstName = "Niklas",
                LastName = "Eriksson",
                Email = "Niklas.Eriksson@datanaut.com",
                FKMemberRoleID = employeeRole.RoleID,
                FKSkillID = qualityAssuranceSkill.SkillID
            };

            context.TeamMembers.AddRange(robin, adcha, niklas);
            context.SaveChanges();

            // Project
            var jupiterProject = new Project
            {
                ProjectName = "Jupiter Research Platform",
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                FKProjectManagerID = adcha.TeamMemberID,
                FKProjectStatusID = inProgress.StatusID,
                Budget = 10_000_000
            };

            var marsProject = new Project
            {
                ProjectName = "Mars Habitat",
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-100)),
                FKProjectManagerID = robin.TeamMemberID,
                FKProjectStatusID = notStarted.StatusID,
                Budget = 100_000_000
            };

            context.Projects.AddRange(jupiterProject, marsProject);
            context.SaveChanges();

            // Project Team
            context.ProjectTeams.AddRange(
                new ProjectTeam { FKProjectID = jupiterProject.ProjectID, FKTeamMemberID = adcha.TeamMemberID },
                new ProjectTeam { FKProjectID = jupiterProject.ProjectID, FKTeamMemberID = niklas.TeamMemberID },
                new ProjectTeam { FKProjectID = marsProject.ProjectID, FKTeamMemberID = niklas.TeamMemberID },
                new ProjectTeam { FKProjectID = marsProject.ProjectID, FKTeamMemberID = adcha.TeamMemberID },
                new ProjectTeam { FKProjectID = marsProject.ProjectID, FKTeamMemberID = robin.TeamMemberID }
            );

            context.SaveChanges();

            // Equipment
            var antenna = new Equipment
            {
                EquipmentName = "Satallite Antenna",
                EquipmentQuantity = 3,
                SerialNumber = "ANT-001",
                Vendor = "SpaceX",
                EquipmentCondition = "New",
                Cost = 500_000
            };

            var rover = new Equipment
            {
                EquipmentName = "Mars Rover",
                EquipmentQuantity = 2,
                SerialNumber = "ROV-001",
                Vendor = "NASA",
                EquipmentCondition = "Used",
                Cost = 500_000_000
            };

            context.Equipment.AddRange(antenna, rover);
            context.SaveChanges();

            // Software
            var orbitSimulator = new Software
            {
                SoftwareName = "Orbit Simulator",
                SoftwareQuantity = 1,
                SoftwareVersion = "v10.1",
                Vendor = "Isar Aerospace",
                Operationsystem = "Windows",
                RequireLicense = true,
                Cost = 100_000
            };

            var habitatPlanner = new Software
            {
                SoftwareName = "Habitat Planner",
                SoftwareQuantity = 1,
                SoftwareVersion = "v2.1",
                Vendor = "Blue Origin",
                Operationsystem = "Windows",
                RequireLicense = true,
                Cost = 300_000
            };

            context.Softwares.AddRange(orbitSimulator, habitatPlanner);
            context.SaveChanges();

            // License
            var orbitLicense = new License
            {
                LicenseName = "Orbit Simulator Pro License",
                LicenseKey = "ORB-PRO-2025",
                LicenseQuantity = 1,
                Vendor = "Isar Aerospace",
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                Cost = 50_000
            };

            var habitatLicense = new License
            {
                LicenseName = "Habitat Planner License",
                LicenseKey = "HAB-PRO-2025",
                LicenseQuantity = 1,
                Vendor = "Blue Origin",
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                Cost = 75_000
            };

            context.Licenses.AddRange(orbitLicense, habitatLicense);
            context.SaveChanges();

            // Project Resources
            context.ProjectResources.AddRange(
                new ProjectResource { FKProjectID = jupiterProject.ProjectID, FKEquipmentID = antenna.EquipmentID },
                new ProjectResource { FKProjectID = jupiterProject.ProjectID, FKSoftwareID = orbitSimulator.SoftwareID },
                new ProjectResource { FKProjectID = jupiterProject.ProjectID, FKLicenseID = orbitLicense.LicenseID },
                new ProjectResource { FKProjectID = marsProject.ProjectID, FKEquipmentID = rover.EquipmentID },
                new ProjectResource { FKProjectID = marsProject.ProjectID, FKSoftwareID = habitatPlanner.SoftwareID },
                new ProjectResource { FKProjectID = marsProject.ProjectID, FKLicenseID = habitatLicense.LicenseID }
            );

            context.SaveChanges();

            // Time log
            context.TimeLogs.AddRange(
                new TimeLog
                {
                    FKProjectID = jupiterProject.ProjectID,
                    FKTeamMemberID = adcha.TeamMemberID,
                    FKActivityID = developmentActivity.ActivityID,
                    LogDate = DateTime.Now.AddDays(-5),
                    TimeSpent = new TimeSpan(4, 0, 0)
                },
                new TimeLog
                {
                    FKProjectID = jupiterProject.ProjectID,
                    FKTeamMemberID = niklas.TeamMemberID,
                    FKActivityID = testingActivity.ActivityID,
                    LogDate = DateTime.Now.AddDays(-10),
                    TimeSpent = new TimeSpan(2, 20, 0)
                },
                new TimeLog
                {
                    FKProjectID = marsProject.ProjectID,
                    FKTeamMemberID = robin.TeamMemberID,
                    FKActivityID = developmentActivity.ActivityID,
                    LogDate = DateTime.Now.AddDays(-30),
                    TimeSpent = new TimeSpan(7, 0, 0)
                },
                new TimeLog
                {
                    FKProjectID = marsProject.ProjectID,
                    FKTeamMemberID = niklas.TeamMemberID,
                    FKActivityID = testingActivity.ActivityID,
                    LogDate = DateTime.Now.AddDays(-40),
                    TimeSpent = new TimeSpan(5, 30, 0)
                },
                new TimeLog
                {
                    FKProjectID = marsProject.ProjectID,
                    FKTeamMemberID = adcha.TeamMemberID,
                    FKActivityID = documentationActivity.ActivityID,
                    LogDate = DateTime.Now.AddDays(-50),
                    TimeSpent = new TimeSpan(6, 0, 0)
                }
            );

            context.SaveChanges();
        }
    }
}
