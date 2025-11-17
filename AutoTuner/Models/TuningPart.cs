using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class TuningPart
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 1000)]
    [Display(Name = "Power Gain")]
    public int PowerGain { get; set; }

    [Range(0, 1000)]
    [Display(Name = "Torque Gain")]
    public int TorqueGain { get; set; }

    [Range(-100, 100)]
    [Display(Name = "Efficiency Impact")]
    public double EfficiencyImpact { get; set; }

    [Range(0, 100000)]
    public decimal Cost { get; set; }

    [Required]
    [Display(Name = "Recommended For Style")]

    public DrivingStyle RecommendedForStyle { get; set; }

    [Display(Name = "Safety critical?")]
    public bool IsSafetyCritical { get; set; }

    public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
}
