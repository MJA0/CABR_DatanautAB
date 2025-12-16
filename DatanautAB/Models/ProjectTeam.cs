using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("ProjectTeam")]
public partial class ProjectTeam
{
    [Key]
    public int ProjectTeamID { get; set; }

    public int FKProjectID { get; set; }

    public int FKTeamMemberID { get; set; }

    [ForeignKey("FKProjectID")]
    [InverseProperty("ProjectTeams")]
    public virtual Project FKProject { get; set; } = null!;

    [ForeignKey("FKTeamMemberID")]
    [InverseProperty("ProjectTeams")]
    public virtual TeamMember FKTeamMember { get; set; } = null!;
}
