using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("Software")]
[Index("SoftwareName", Name = "UQ_Software_SoftwareName", IsUnique = true)]
public partial class Software
{
    [Key]
    public int SoftwareID { get; set; }

    [StringLength(100)]
    public string SoftwareName { get; set; } = null!;

    public int SoftwareQuantity { get; set; }

    [StringLength(100)]
    public string? SoftwareVersion { get; set; }

    [StringLength(100)]
    public string? Vendor { get; set; }

    [StringLength(100)]
    public string? Operationsystem { get; set; }

    public bool RequireLicense { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Cost { get; set; }

    [InverseProperty("FKSoftware")]
    public virtual ICollection<ProjectResource> ProjectResources { get; set; } = new List<ProjectResource>();
}
