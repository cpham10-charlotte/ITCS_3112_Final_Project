using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private List<Recipe> _recipes = new List<Recipe>();
    
    public List<Recipe> GetAll()
    {
        return _recipes;
    }

    public Recipe GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Enter a valid id.");
        }

        Recipe recipe = _recipes.FirstOrDefault(r => r.Id == id);

        if (recipe == null)
        {
            throw new Exception("Recipe not found.");
        }

        return recipe;
    }

    public void Add(Recipe recipe)
    {
        if (recipe == null)
        {
            throw new ArgumentNullException(nameof(recipe));
        }

        if (_recipes.Any(r => r.Id == recipe.Id))
        {
            throw new Exception("Recipe with this Id already exists.");
        }

        _recipes.Add(recipe);
    }
}