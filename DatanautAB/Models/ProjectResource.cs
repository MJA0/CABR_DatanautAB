using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("ProjectResource")]
public partial class ProjectResource
{
    [Key]
    public int ResourceID { get; set; }

    public int FKProjectID { get; set; }

    public int? FKLicenseID { get; set; }

    public int? FKSoftwareID { get; set; }

    public int? FKEquipmentID { get; set; }

    [ForeignKey("FKEquipmentID")]
    [InverseProperty("ProjectResources")]
    public virtual Equipment? FKEquipment { get; set; }

    [ForeignKey("FKLicenseID")]
    [InverseProperty("ProjectResources")]
    public virtual License? FKLicense { get; set; }

    [ForeignKey("FKProjectID")]
    [InverseProperty("ProjectResources")]
    public virtual Project FKProject { get; set; } = null!;

    [ForeignKey("FKSoftwareID")]
    [InverseProperty("ProjectResources")]
    public virtual Software? FKSoftware { get; set; }
}
