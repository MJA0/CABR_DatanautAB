using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("Skill")]
[Index("SkillName", Name = "UQ_Skill_SkillName", IsUnique = true)]
public partial class Skill
{
    [Key]
    public int SkillID { get; set; }

    [StringLength(100)]
    public string SkillName { get; set; } = null!;

    [InverseProperty("FKSkill")]
    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
