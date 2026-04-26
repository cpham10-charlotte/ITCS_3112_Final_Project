using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Enums;

namespace ITCS_3112_Final_Project.Services;

public class RecipeCreationService : IRecipeCreationService
{
    private IRecipeRepository _recipeRepo;
    private IIngredientRepository _ingredientRepo;

    public RecipeCreationService(IRecipeRepository recipeRepo, IIngredientRepository ingredientRepo)
    {
        _recipeRepo = recipeRepo;
        _ingredientRepo = ingredientRepo;
    }

    public Recipe CreateRecipe(User user, string name, string id, VeganEnum vegan, List<Ingredient> ingredients)
    {
        Recipe newRecipe = new Recipe(name, id, vegan, ingredients);
        
        _recipeRepo.Add(newRecipe);

        return newRecipe;
    }
}