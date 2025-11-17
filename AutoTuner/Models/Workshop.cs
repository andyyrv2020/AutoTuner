using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models;

public class Workshop
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [StringLength(150)]
    public string? Specialization { get; set; }
}
