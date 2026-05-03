using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Contracts;

public interface IRecommendationService
{
    List<Recipe> Recommend(string userId);
}