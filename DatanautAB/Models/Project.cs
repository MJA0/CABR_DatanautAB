using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("Project")]
[Index("ProjectName", Name = "UQ_Project_ProjectName", IsUnique = true)]
public partial class Project
{
    [Key]
    public int ProjectID { get; set; }

    public int FKProjectStatusID { get; set; }

    public int FKProjectManagerID { get; set; }

    [StringLength(100)]
    public string ProjectName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Budget { get; set; }

    [ForeignKey("FKProjectManagerID")]
    [InverseProperty("Projects")]
    public virtual TeamMember FKProjectManager { get; set; } = null!;

    [ForeignKey("FKProjectStatusID")]
    [InverseProperty("Projects")]
    public virtual ProjectStatus FKProjectStatus { get; set; } = null!;

    [InverseProperty("FKProject")]
    public virtual ICollection<ProjectResource> ProjectResources { get; set; } = new List<ProjectResource>();

    [InverseProperty("FKProject")]
    public virtual ICollection<ProjectTeam> ProjectTeams { get; set; } = new List<ProjectTeam>();

    [InverseProperty("FKProject")]
    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
