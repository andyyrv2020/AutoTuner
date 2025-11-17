using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class Recommendation
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public int TuningPartId { get; set; }

    [Range(0, 2000)]
    [Display(Name = "Predicted Power")]
    public int PredictedPower { get; set; }

    [Range(0, 2000)]
    [Display(Name = "Predicted Torque")]
    public int PredictedTorque { get; set; }

    [Range(0, 1000000)]
    [Display(Name = "Total Cost")]
    public decimal TotalCost { get; set; }

    public OptimizationGoal Goal { get; set; }

    [Range(0, 1000000)]
    public decimal? Budget { get; set; }

    [Display(Name = "Include Safety Parts?")]
    public bool IncludeSafetyParts { get; set; }

    [Display(Name = "Date Generated")]
    public DateTime DateGenerated { get; set; }

    public Car? Car { get; set; }

    [Display(Name = "Tuning Part")]
    public TuningPart? TuningPart { get; set; }
}
