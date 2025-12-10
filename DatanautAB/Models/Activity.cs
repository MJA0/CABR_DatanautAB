using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

[Table("Activity")]
[Index("ActivityName", Name = "UQ_Activity_ActivityName", IsUnique = true)]
public partial class Activity
{
    [Key]
    public int ActivityID { get; set; }

    [StringLength(100)]
    public string ActivityName { get; set; } = null!;

    [InverseProperty("FKActivity")]
    public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
}
