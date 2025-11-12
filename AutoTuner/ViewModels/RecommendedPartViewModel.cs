namespace AutoTuner.ViewModels;

public class RecommendedPartViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int PowerGain { get; set; }
    public int TorqueGain { get; set; }
    public double EfficiencyImpact { get; set; }
    public decimal Cost { get; set; }
    public bool IsSafetyCritical { get; set; }
}
