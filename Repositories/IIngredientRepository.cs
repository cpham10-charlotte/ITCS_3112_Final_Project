using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Repositories;

/// <summary>
/// Data access abstraction for ingredients.
/// </summary>
public interface IIngredientRepository
{
    IReadOnlyList<Ingredient> GetAll();

    Ingredient? GetById(string id);
}
