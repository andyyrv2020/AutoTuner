using AutoTuner.Models;

namespace AutoTuner.ViewModels;

public class RecommendationRequestOptions
{
    public OptimizationGoal Goal { get; set; } = OptimizationGoal.Balanced;

    public decimal? Budget { get; set; }

    public bool IncludeSafetyParts { get; set; } = true;
}
