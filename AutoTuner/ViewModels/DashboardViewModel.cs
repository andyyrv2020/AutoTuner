using AutoTuner.Models;

namespace AutoTuner.ViewModels;

public class DashboardViewModel
{
    public List<Car> Cars { get; set; } = new();
    public List<Recommendation> Recommendations { get; set; } = new();
    public List<PerformanceHistory> PerformanceHistory { get; set; } = new();
    public Dictionary<string, decimal> CostByCategory { get; set; } = new();
    public Dictionary<string, double> EfficiencyByCategory { get; set; } = new();
    public decimal TotalBudget { get; set; }
    public double AverageEfficiency { get; set; }
}
