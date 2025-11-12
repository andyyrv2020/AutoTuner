using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AutoTuner.Models;

public class Car
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    [Range(1950, 2100)]
    public int Year { get; set; }

    [Required]
    [StringLength(100)]
    public string EngineType { get; set; } = string.Empty;

    [Range(0, 2000)]
    public int HorsePower { get; set; }

    [Range(0, 2000)]
    public int Torque { get; set; }

    [Range(0, 5000)]
    public int Weight { get; set; }

    [StringLength(100)]
    public string Drivetrain { get; set; } = string.Empty;

    [Required]
    public DrivingStyle DrivingStyle { get; set; }

    [ForeignKey(nameof(UserId))]
    public IdentityUser? User { get; set; }

    public ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public ICollection<PerformanceHistory> PerformanceHistory { get; set; } = new List<PerformanceHistory>();
}
