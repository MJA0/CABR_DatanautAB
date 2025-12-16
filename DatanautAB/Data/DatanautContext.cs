using System;
using System.Collections.Generic;
using DatanautAB.Models;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Data;

public partial class DatanautContext : DbContext
{
    public DatanautContext()
    {
    }

    public DatanautContext(DbContextOptions<DatanautContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<MemberRole> MemberRoles { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectResource> ProjectResources { get; set; }

    public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }

    public virtual DbSet<ProjectTeam> ProjectTeams { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Software> Softwares { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<TimeLog> TimeLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityID).HasName("PK__Activity__45F4A7F1E9D50185");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentID).HasName("PK__Equipmen__3447459910855EE9");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.LicenseID).HasName("PK__License__72D600A253AD2671");
        });

        modelBuilder.Entity<MemberRole>(entity =>
        {
            entity.HasKey(e => e.RoleID).HasName("PK__MemberRo__8AFACE3A41D5CFDA");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectID).HasName("PK__Project__761ABED016C14751");

            entity.HasOne(d => d.FKProjectManager).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Manager");

            entity.HasOne(d => d.FKProjectStatus).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectStatus");
        });

        modelBuilder.Entity<ProjectResource>(entity =>
        {
            entity.HasKey(e => e.ResourceID).HasName("PK__ProjectR__4ED1814F8EC107D9");

            entity.ToTable("ProjectResource", tb => tb.HasTrigger("software_quantity_update"));

            entity.HasOne(d => d.FKEquipment).WithMany(p => p.ProjectResources).HasConstraintName("FK_Resource_Equipment");

            entity.HasOne(d => d.FKLicense).WithMany(p => p.ProjectResources).HasConstraintName("FK_Resource_License");

            entity.HasOne(d => d.FKProject).WithMany(p => p.ProjectResources)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resource_Project");

            entity.HasOne(d => d.FKSoftware).WithMany(p => p.ProjectResources).HasConstraintName("FK_Resource_Software");
        });

        modelBuilder.Entity<ProjectStatus>(entity =>
        {
            entity.HasKey(e => e.StatusID).HasName("PK__ProjectS__C8EE204382BB8D75");
        });

        modelBuilder.Entity<ProjectTeam>(entity =>
        {
            entity.HasKey(e => e.ProjectTeamID).HasName("PK__ProjectT__B043C67466C3BC9F");

            entity.ToTable("ProjectTeam", tb =>
                {
                    tb.HasTrigger("check_overlapping_skill_role");
                    tb.HasTrigger("check_teamMember");
                });

            entity.HasOne(d => d.FKProject).WithMany(p => p.ProjectTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTeam_Project");

            entity.HasOne(d => d.FKTeamMember).WithMany(p => p.ProjectTeams)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTeam_TeamMember");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillID).HasName("PK__Skill__DFA091E793FA8D59");
        });

        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.SoftwareID).HasName("PK__Software__25EDB8DC8F693AD9");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.TeamMemberID).HasName("PK__TeamMemb__C7C09285F51870BB");

            entity.HasOne(d => d.FKMemberRole).WithMany(p => p.TeamMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamMember_MemberRole");

            entity.HasOne(d => d.FKSkill).WithMany(p => p.TeamMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeamMember_Skill");
        });

        modelBuilder.Entity<TimeLog>(entity =>
        {
            entity.HasKey(e => e.TimeLogID).HasName("PK__TimeLogs__26E437B761B036A2");

            entity.HasOne(d => d.FKActivity).WithMany(p => p.TimeLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeLogs_Activity");

            entity.HasOne(d => d.FKProject).WithMany(p => p.TimeLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeLogs_Project");

            entity.HasOne(d => d.FKTeamMember).WithMany(p => p.TimeLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeLogs_TeamMember");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
