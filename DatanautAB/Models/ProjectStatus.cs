using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("ProjectStatus")]
[Index("StatusName", Name = "UQ_ProjectStatus_StatusName", IsUnique = true)]
public partial class ProjectStatus
{
    [Key]
    public int StatusID { get; set; }

    [StringLength(100)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("FKProjectStatus")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
