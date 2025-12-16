using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("License")]
[Index("LicenseKey", Name = "UQ_License_LicenseKey", IsUnique = true)]
[Index("LicenseName", Name = "UQ_License_LicenseName", IsUnique = true)]
public partial class License
{
    [Key]
    public int LicenseID { get; set; }

    [StringLength(100)]
    public string LicenseName { get; set; } = null!;

    [StringLength(100)]
    public string LicenseKey { get; set; } = null!;

    public int LicenseQuantity { get; set; }

    [StringLength(100)]
    public string? LicenseVersion { get; set; }

    [StringLength(100)]
    public string? Vendor { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Cost { get; set; }

    [InverseProperty("FKLicense")]
    public virtual ICollection<ProjectResource> ProjectResources { get; set; } = new List<ProjectResource>();
}
