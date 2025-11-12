using AutoTuner.ViewModels;

namespace AutoTuner.Services;

public interface IRecommendationService
{
    Task<RecommendationResultViewModel?> GenerateRecommendationsAsync(int carId);
    Task<RecommendationResultViewModel?> GetRecommendationsAsync(int carId);
}
