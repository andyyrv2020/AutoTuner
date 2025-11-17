using AutoTuner.ViewModels;

namespace AutoTuner.Services;

public interface IRecommendationService
{
    Task<RecommendationResultViewModel?> GenerateRecommendationsAsync(int carId, RecommendationRequestOptions? options = null);
    Task<RecommendationResultViewModel?> GetRecommendationsAsync(int carId, RecommendationRequestOptions? options = null);
}
