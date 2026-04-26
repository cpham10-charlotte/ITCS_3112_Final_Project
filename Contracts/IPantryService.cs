using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Contracts;

public interface IPantryService
{
    IReadOnlyList<Ingredient> ViewPantry(string userId);

    bool AddIngredient(string userId, string ingredientId);

    bool RemoveIngredient(string userId, string ingredientId);
}

