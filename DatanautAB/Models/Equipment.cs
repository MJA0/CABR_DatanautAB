using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Index("EquipmentName", Name = "UQ_Equipment_EquipmentName", IsUnique = true)]
[Index("SerialNumber", Name = "UQ_Equipment_SerialNumber", IsUnique = true)]
public partial class Equipment
{
    [Key]
    public int EquipmentID { get; set; }

    [StringLength(100)]
    public string EquipmentName { get; set; } = null!;

    public int EquipmentQuantity { get; set; }

    [StringLength(100)]
    public string SerialNumber { get; set; } = null!;

    [StringLength(100)]
    public string? Vendor { get; set; }

    [StringLength(100)]
    public string? EquipmentCondition { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Cost { get; set; }

    [InverseProperty("FKEquipment")]
    public virtual ICollection<ProjectResource> ProjectResources { get; set; } = new List<ProjectResource>();
}
