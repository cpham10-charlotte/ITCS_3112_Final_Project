using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Contracts;

public abstract class PantryService
{
    public abstract IReadOnlyList<Ingredient> ViewPantry(string userId);

    public abstract void UpdatePantry(string userId, string ingredientId);
}

