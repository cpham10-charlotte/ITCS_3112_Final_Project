using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Services;

public class RecommendationService : IRecommendationService
{
    private IRecipeRepository _recipeRepo;
    private IIngredientRepository _ingredientRepo;
    private IPantryRepository _pantryRepo;

    public RecommendationService(IRecipeRepository recipeRepo, IIngredientRepository ingredientRepo, IPantryRepository pantryRepo)
    {
        _recipeRepo = recipeRepo;
        _ingredientRepo = ingredientRepo;
        _pantryRepo = pantryRepo;
    }

    public List<Recipe> Recommend(string userId)
    {
        IPantryService pantryService = new PantryService(_pantryRepo, _ingredientRepo);
        IReadOnlyList<Ingredient> userPantry = pantryService.ViewPantry(userId);
        List<Recipe> recommendations = [];

        foreach (Recipe recipe in _recipeRepo.GetAll())
        {
            float matches = 0;
            float count = 0;
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                count += 1;
                if (userPantry.Contains(ingredient))
                {
                    matches += 1;
                }
            }

            if (matches / count >= .75)
            {
                recommendations.Add(recipe);
            }
        }
        
        return recommendations;
    }
}