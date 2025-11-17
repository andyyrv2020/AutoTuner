using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class PerformanceHistory
{
    public int Id { get; set; }

    public int CarId { get; set; }

    [Range(0, 2000)]
    public int OldPower { get; set; }

    [Range(0, 2000)]
    public int NewPower { get; set; }

    [Range(0, 2000)]
    public int OldTorque { get; set; }

    [Range(0, 2000)]
    public int NewTorque { get; set; }

    public DateTime DateApplied { get; set; }

    public Car? Car { get; set; }
}
