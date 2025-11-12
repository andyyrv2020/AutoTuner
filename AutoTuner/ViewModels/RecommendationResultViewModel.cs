using System;
using System.Collections.Generic;
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

    public OptimizationGoal Goal { get; set; }
    public decimal? Budget { get; set; }
    public bool IncludeSafetyParts { get; set; }
    public bool SafetyPartsIncluded { get; set; }
    public bool BudgetExceeded { get; set; }
    public double? BudgetUsagePercent { get; set; }
    public double PowerToWeightBefore { get; set; }
    public double PowerToWeightAfter { get; set; }
    public double EstimatedZeroToHundredBefore { get; set; }
    public double EstimatedZeroToHundredAfter { get; set; }
    public decimal CostPerHorsepower { get; set; }
    public decimal CostPerTorque { get; set; }
    public int TotalPowerGain { get; set; }
    public int TotalTorqueGain { get; set; }
    public bool UsedFallbackParts { get; set; }
    public Dictionary<string, decimal> CostByCategory { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public List<StrategyInsightViewModel> Insights { get; set; } = new();
}
