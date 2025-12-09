using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatanautAB.Models;

public partial class TimeLog
{
    [Key]
    public int TimeLogID { get; set; }

    public int FKProjectID { get; set; }

    public int FKActivityID { get; set; }

    public int FKTeamMemberID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    public TimeOnly TimeSpent { get; set; }

    [ForeignKey("FKActivityID")]
    [InverseProperty("TimeLogs")]
    public virtual Activity FKActivity { get; set; } = null!;

    [ForeignKey("FKProjectID")]
    [InverseProperty("TimeLogs")]
    public virtual Project FKProject { get; set; } = null!;

    [ForeignKey("FKTeamMemberID")]
    [InverseProperty("TimeLogs")]
    public virtual TeamMember FKTeamMember { get; set; } = null!;
}
