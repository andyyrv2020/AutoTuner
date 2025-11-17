using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class PerformanceHistory
{
    public int Id { get; set; }

    public int CarId { get; set; }

    [Range(0, 2000)]
    [Display(Name = "Old Power")]
    public int OldPower { get; set; }

    [Range(0, 2000)]
    [Display(Name = "New Power")]
    public int NewPower { get; set; }

    [Range(0, 2000)]
    [Display(Name = "Old Torque")]
    public int OldTorque { get; set; }

    [Range(0, 2000)]
    [Display(Name = "New Torque")]
    public int NewTorque { get; set; }

    [Display(Name = "Date Applied")]
    public DateTime DateApplied { get; set; }

    public Car? Car { get; set; }
}
