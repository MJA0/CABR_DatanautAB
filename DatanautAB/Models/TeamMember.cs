using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("TeamMember")]
[Index("Email", Name = "UQ_TeamMember_Email", IsUnique = true)]
public partial class TeamMember
{
    [Key]
    public int TeamMemberID { get; set; }

    public int FKMemberRoleID { get; set; }

    public int FKSkillID { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [ForeignKey("FKMemberRoleID")]
    [InverseProperty("TeamMembers")]
    public virtual MemberRole FKMemberRole { get; set; } = null!;

    [ForeignKey("FKSkillID")]
    [InverseProperty("TeamMembers")]
    public virtual Skill FKSkill { get; set; } = null!;

    [InverseProperty("FKTeamMember")]
    public virtual ICollection<ProjectTeam> ProjectTeams { get; set; } = new List<ProjectTeam>();

    [InverseProperty("FKProjectManager")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("FKTeamMember")]
    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
