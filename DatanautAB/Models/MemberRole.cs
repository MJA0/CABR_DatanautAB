using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("MemberRole")]
[Index("RoleName", Name = "UQ_MemberRole_RoleName", IsUnique = true)]
public partial class MemberRole
{
    [Key]
    public int RoleID { get; set; }

    [StringLength(100)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("FKMemberRole")]
    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
