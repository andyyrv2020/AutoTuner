using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class Recommendation
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public int TuningPartId { get; set; }

    [Range(0, 2000)]
    public int PredictedPower { get; set; }

    [Range(0, 2000)]
    public int PredictedTorque { get; set; }

    [Range(0, 1000000)]
    public decimal TotalCost { get; set; }

    public OptimizationGoal Goal { get; set; }

    [Range(0, 1000000)]
    public decimal? Budget { get; set; }

    public bool IncludeSafetyParts { get; set; }

    public DateTime DateGenerated { get; set; }

    public Car? Car { get; set; }

    public TuningPart? TuningPart { get; set; }
}
