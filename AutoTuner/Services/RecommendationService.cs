using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<RecommendationResultViewModel?> GenerateRecommendationsAsync(int carId, RecommendationRequestOptions? options = null)
    {
        options ??= new RecommendationRequestOptions();

        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        if (car is null)
        {
            return null;
        }

        var allParts = await _context.TuningParts.ToListAsync();
        var filteredParts = FilterPartsByStyle(car, allParts, out var styleFallback);
        var selectedParts = ApplyStrategy(filteredParts, options, out var strategyFallback, out var forcedSafetyInclusion);

        if (!selectedParts.Any())
        {
            selectedParts = allParts
                .OrderByDescending(p => p.PowerGain + p.TorqueGain)
                .ThenBy(p => p.Cost)
                .Take(3)
                .ToList();
            strategyFallback = true;
        }

        var predictedPower = car.HorsePower + selectedParts.Sum(p => p.PowerGain);
        var predictedTorque = car.Torque + selectedParts.Sum(p => p.TorqueGain);
        var averageEfficiency = selectedParts.Any() ? selectedParts.Average(p => p.EfficiencyImpact) : 0;
        var totalCost = selectedParts.Sum(p => p.Cost);

        var existing = await _context.Recommendations
            .Where(r => r.CarId == carId
                        && r.Goal == options.Goal
                        && r.IncludeSafetyParts == options.IncludeSafetyParts
                        && r.Budget == options.Budget)
            .ToListAsync();

        if (existing.Any())
        {
            _context.Recommendations.RemoveRange(existing);
        }

        foreach (var part in selectedParts)
        {
            _context.Recommendations.Add(new Recommendation
            {
                CarId = carId,
                TuningPartId = part.Id,
                PredictedPower = predictedPower,
                PredictedTorque = predictedTorque,
                TotalCost = totalCost,
                Goal = options.Goal,
                Budget = options.Budget,
                IncludeSafetyParts = options.IncludeSafetyParts,
                DateGenerated = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();

        return BuildResult(car, selectedParts, options, predictedPower, predictedTorque, averageEfficiency, totalCost, strategyFallback || styleFallback, forcedSafetyInclusion);
    }

    public async Task<RecommendationResultViewModel?> GetRecommendationsAsync(int carId, RecommendationRequestOptions? options = null)
    {
        options ??= new RecommendationRequestOptions();

        var car = await _context.Cars
            .Include(c => c.Recommendations.Where(r => r.Goal == options.Goal
                                                       && r.IncludeSafetyParts == options.IncludeSafetyParts
                                                       && r.Budget == options.Budget))
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

        return BuildResult(car, parts, options, predictedPower, predictedTorque, averageEfficiency, totalCost, false, false);
    }

    private static List<TuningPart> FilterPartsByStyle(Car car, List<TuningPart> parts, out bool usedFallback)
    {
        var filtered = parts
            .Where(p => p.RecommendedForStyle == car.DrivingStyle
                        || p.RecommendedForStyle == DrivingStyle.Daily
                        || (car.DrivingStyle == DrivingStyle.Daily && p.RecommendedForStyle == DrivingStyle.Eco))
            .ToList();

        if (filtered.Any())
        {
            usedFallback = false;
            return filtered;
        }

        usedFallback = true;
        return parts;
    }

    private static List<TuningPart> ApplyStrategy(List<TuningPart> parts, RecommendationRequestOptions options, out bool usedFallback, out bool forcedSafetyInclusion)
    {
        usedFallback = false;
        forcedSafetyInclusion = false;

        if (!parts.Any())
        {
            usedFallback = true;
            return parts;
        }

        var candidatePool = parts;
        if (!options.IncludeSafetyParts)
        {
            var nonSafety = parts.Where(p => !p.IsSafetyCritical).ToList();
            if (nonSafety.Any())
            {
                candidatePool = nonSafety;
            }
            else
            {
                forcedSafetyInclusion = true;
            }
        }

        var prioritized = options.Goal switch
        {
            OptimizationGoal.MaxPower => candidatePool
                .OrderByDescending(p => p.PowerGain)
                .ThenByDescending(p => p.TorqueGain)
                .ThenBy(p => p.Cost)
                .ToList(),
            OptimizationGoal.MaxEfficiency => candidatePool
                .OrderByDescending(p => p.EfficiencyImpact)
                .ThenBy(p => p.Cost)
                .ToList(),
            OptimizationGoal.BudgetFocused => candidatePool
                .OrderByDescending(CalculateBudgetScore)
                .ThenBy(p => p.Cost)
                .ToList(),
            _ => candidatePool
                .OrderByDescending(p => p.PowerGain * 1.5 + p.TorqueGain + p.EfficiencyImpact * 2)
                .ThenBy(p => p.Cost)
                .ToList()
        };

        var selected = new List<TuningPart>();
        decimal runningCost = 0m;
        var budget = options.Budget is > 0 ? options.Budget : null;

        foreach (var part in prioritized)
        {
            if (budget.HasValue && runningCost + part.Cost > budget.Value)
            {
                continue;
            }

            selected.Add(part);
            runningCost += part.Cost;

            if (options.Goal == OptimizationGoal.MaxPower && selected.Count >= 5)
            {
                break;
            }

            if (options.Goal == OptimizationGoal.MaxEfficiency && selected.Count >= 4)
            {
                break;
            }

            if (options.Goal == OptimizationGoal.BudgetFocused && budget.HasValue && runningCost >= budget.Value * 0.9m)
            {
                break;
            }
        }

        if (!selected.Any())
        {
            var cheapest = prioritized.OrderBy(p => p.Cost).FirstOrDefault();
            if (cheapest is not null)
            {
                selected.Add(cheapest);
                usedFallback = true;
            }
        }

        return selected;
    }

    private static double CalculateBudgetScore(TuningPart part)
    {
        var gainScore = part.PowerGain * 1.2 + part.TorqueGain;
        var efficiencyBonus = part.EfficiencyImpact * 3;
        var costPenalty = Math.Log10((double)part.Cost + 10);
        return gainScore + efficiencyBonus - costPenalty;
    }

    private static double CalculatePowerToWeight(double power, double weightKg)
    {
        var tons = weightKg <= 0 ? 1 : weightKg / 1000.0;
        return power / tons;
    }

    private static double EstimateZeroToHundred(double powerToWeight)
    {
        var baseline = 8.5 - powerToWeight * 0.35;
        return Math.Round(Math.Clamp(baseline, 2.7, 12.0), 1);
    }

    private static RecommendationResultViewModel BuildResult(
        Car car,
        List<TuningPart> parts,
        RecommendationRequestOptions options,
        int predictedPower,
        int predictedTorque,
        double averageEfficiency,
        decimal totalCost,
        bool usedFallback,
        bool forcedSafetyInclusion)
    {
        var totalPowerGain = predictedPower - car.HorsePower;
        var totalTorqueGain = predictedTorque - car.Torque;
        var powerToWeightBefore = CalculatePowerToWeight(car.HorsePower, car.Weight);
        var powerToWeightAfter = CalculatePowerToWeight(predictedPower, car.Weight);
        var zeroToHundredBefore = EstimateZeroToHundred(powerToWeightBefore);
        var zeroToHundredAfter = EstimateZeroToHundred(powerToWeightAfter);

        var costPerHorsepower = totalPowerGain > 0 ? decimal.Round(totalCost / totalPowerGain, 2) : 0m;
        var costPerTorque = totalTorqueGain > 0 ? decimal.Round(totalCost / totalTorqueGain, 2) : 0m;

        var budgetExceeded = options.Budget.HasValue && options.Budget.Value > 0 && totalCost > options.Budget.Value;
        double? budgetUsagePercent = null;
        if (options.Budget.HasValue && options.Budget.Value > 0)
        {
            budgetUsagePercent = Math.Round((double)(totalCost / options.Budget.Value) * 100, 1);
        }

        var costByCategory = parts
            .GroupBy(p => p.Category)
            .ToDictionary(g => g.Key, g => g.Sum(p => p.Cost), StringComparer.OrdinalIgnoreCase);

        var result = new RecommendationResultViewModel
        {
            Car = car,
            PredictedPower = predictedPower,
            PredictedTorque = predictedTorque,
            AverageEfficiencyImpact = Math.Round(averageEfficiency, 2),
            TotalCost = decimal.Round(totalCost, 2),
            Parts = parts.Select(p => new RecommendedPartViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                PowerGain = p.PowerGain,
                TorqueGain = p.TorqueGain,
                EfficiencyImpact = p.EfficiencyImpact,
                Cost = p.Cost,
                IsSafetyCritical = p.IsSafetyCritical
            }).ToList(),
            Goal = options.Goal,
            Budget = options.Budget,
            IncludeSafetyParts = options.IncludeSafetyParts,
            SafetyPartsIncluded = parts.Any(p => p.IsSafetyCritical),
            BudgetExceeded = budgetExceeded,
            BudgetUsagePercent = budgetUsagePercent,
            PowerToWeightBefore = Math.Round(powerToWeightBefore, 2),
            PowerToWeightAfter = Math.Round(powerToWeightAfter, 2),
            EstimatedZeroToHundredBefore = zeroToHundredBefore,
            EstimatedZeroToHundredAfter = zeroToHundredAfter,
            CostPerHorsepower = costPerHorsepower,
            CostPerTorque = costPerTorque,
            TotalPowerGain = totalPowerGain,
            TotalTorqueGain = totalTorqueGain,
            UsedFallbackParts = usedFallback,
            CostByCategory = costByCategory
        };

        result.Insights = BuildInsights(result, forcedSafetyInclusion);
        return result;
    }

    private static List<StrategyInsightViewModel> BuildInsights(RecommendationResultViewModel result, bool forcedSafetyInclusion)
    {
        var insights = new List<StrategyInsightViewModel>
        {
            new()
            {
                Title = "Output boost",
                Description = $"Estimated gain of +{result.TotalPowerGain} hp and +{result.TotalTorqueGain} Nm.",
                Icon = "fa-gauge-high"
            },
            new()
            {
                Title = "Acceleration outlook",
                Description = $"0-100 km/h drops from ~{result.EstimatedZeroToHundredBefore:F1}s to ~{result.EstimatedZeroToHundredAfter:F1}s.",
                Icon = "fa-road"
            }
        };

        if (result.AverageEfficiencyImpact > 0)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Efficiency gains",
                Description = $"Approx. +{result.AverageEfficiencyImpact:F1}% fuel efficiency expected.",
                Icon = "fa-leaf"
            });
        }
        else if (result.AverageEfficiencyImpact < 0)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Fuel trade-off",
                Description = $"Expect around {Math.Abs(result.AverageEfficiencyImpact):F1}% more fuel consumption.",
                Icon = "fa-gas-pump"
            });
        }

        if (result.Budget.HasValue)
        {
            var usage = result.BudgetUsagePercent ?? 0;
            if (result.BudgetExceeded)
            {
                insights.Add(new StrategyInsightViewModel
                {
                    Title = "Budget exceeded",
                    Description = $"Plan overshoots the budget by {(result.TotalCost - result.Budget.Value):C}.",
                    Icon = "fa-triangle-exclamation"
                });
            }
            else
            {
                insights.Add(new StrategyInsightViewModel
                {
                    Title = "Budget usage",
                    Description = $"Approximately {usage:F1}% of the budget is utilized.",
                    Icon = "fa-wallet"
                });
            }
        }

        if (!result.IncludeSafetyParts && result.SafetyPartsIncluded)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Safety override",
                Description = "Safety critical upgrades are still suggested to keep the build balanced.",
                Icon = "fa-shield-halved"
            });
        }
        else if (result.SafetyPartsIncluded)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Confidence",
                Description = "Includes braking/handling upgrades to support the extra performance.",
                Icon = "fa-car-side"
            });
        }

        if (result.CostPerHorsepower > 0)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Cost efficiency",
                Description = $"≈ {result.CostPerHorsepower:C} per horsepower and {result.CostPerTorque:C} per Nm gained.",
                Icon = "fa-scale-balanced"
            });
        }

        if (result.UsedFallbackParts)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Adaptive strategy",
                Description = "Fallback suggestions were used due to limited matches—consider adding more parts to the catalog.",
                Icon = "fa-lightbulb"
            });
        }

        if (forcedSafetyInclusion)
        {
            insights.Add(new StrategyInsightViewModel
            {
                Title = "Safety requirement",
                Description = "No non-safety alternatives matched the filters, so essential safety upgrades remain included.",
                Icon = "fa-helmet-safety"
            });
        }

        return insights;
    }
}
