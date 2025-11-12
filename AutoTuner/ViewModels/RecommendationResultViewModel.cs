using AutoTuner.Models;

namespace AutoTuner.ViewModels;

public class RecommendationResultViewModel
{
    public Car Car { get; set; } = null!;
    public int PredictedPower { get; set; }
    public int PredictedTorque { get; set; }
    public double AverageEfficiencyImpact { get; set; }
    public decimal TotalCost { get; set; }
    public List<RecommendedPartViewModel> Parts { get; set; } = new();
}
