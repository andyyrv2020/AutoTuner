using AutoTuner.Data;
using AutoTuner.Models;
using AutoTuner.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ApplicationDbContext _context;

    public RecommendationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RecommendationResultViewModel?> GenerateRecommendationsAsync(int carId)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        if (car is null)
        {
            return null;
        }

        var allParts = await _context.TuningParts.ToListAsync();
        var parts = allParts
            .Where(p => p.RecommendedForStyle == car.DrivingStyle
                        || p.RecommendedForStyle == DrivingStyle.Daily
                        || (car.DrivingStyle == DrivingStyle.Daily && p.RecommendedForStyle == DrivingStyle.Eco))
            .ToList();

        if (!parts.Any())
        {
            parts = allParts.Take(3).ToList();
        }

        var predictedPower = car.HorsePower + parts.Sum(p => p.PowerGain);
        var predictedTorque = car.Torque + parts.Sum(p => p.TorqueGain);
        var averageEfficiency = parts.Any() ? parts.Average(p => p.EfficiencyImpact) : 0;
        var totalCost = parts.Sum(p => p.Cost);

        var existing = await _context.Recommendations
            .Where(r => r.CarId == carId)
            .ToListAsync();

        if (existing.Any())
        {
            _context.Recommendations.RemoveRange(existing);
        }

        foreach (var part in parts)
        {
            _context.Recommendations.Add(new Recommendation
            {
                CarId = carId,
                TuningPartId = part.Id,
                PredictedPower = predictedPower,
                PredictedTorque = predictedTorque,
                TotalCost = totalCost,
                DateGenerated = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        return BuildResult(car, parts, predictedPower, predictedTorque, averageEfficiency, totalCost);
    }

    public async Task<RecommendationResultViewModel?> GetRecommendationsAsync(int carId)
    {
        var car = await _context.Cars
            .Include(c => c.Recommendations)
            .ThenInclude(r => r.TuningPart)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car is null)
        {
            return null;
        }

        var recommendations = car.Recommendations
            .Where(r => r.TuningPart is not null)
            .ToList();

        if (!recommendations.Any())
        {
            return null;
        }

        var parts = recommendations
            .Select(r => r.TuningPart!)
            .GroupBy(p => p.Id)
            .Select(g => g.First())
            .ToList();

        var predictedPower = recommendations.First().PredictedPower;
        var predictedTorque = recommendations.First().PredictedTorque;
        var totalCost = recommendations.First().TotalCost;
        var averageEfficiency = parts.Any() ? parts.Average(p => p.EfficiencyImpact) : 0;

        return BuildResult(car, parts, predictedPower, predictedTorque, averageEfficiency, totalCost);
    }

    private static RecommendationResultViewModel BuildResult(Car car, IEnumerable<TuningPart> parts, int predictedPower, int predictedTorque, double averageEfficiency, decimal totalCost)
    {
        return new RecommendationResultViewModel
        {
            Car = car,
            PredictedPower = predictedPower,
            PredictedTorque = predictedTorque,
            AverageEfficiencyImpact = Math.Round(averageEfficiency, 2),
            TotalCost = totalCost,
            Parts = parts.Select(p => new RecommendedPartViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                PowerGain = p.PowerGain,
                TorqueGain = p.TorqueGain,
                EfficiencyImpact = p.EfficiencyImpact,
                Cost = p.Cost
            }).ToList()
        };
    }
}
