using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Repositories;

/// <summary>
/// In-memory ingredient catalog. Optionally populated with an initial set of ingredients.
/// </summary>
public sealed class IngredientRepository : IIngredientRepository
{
    private readonly List<Ingredient> _ingredients;

    public IngredientRepository(IEnumerable<Ingredient>? initialIngredients = null)
    {
        _ingredients = initialIngredients?.ToList() ?? new List<Ingredient>();
    }

    public IReadOnlyList<Ingredient> GetAll() => _ingredients.ToList();

    public Ingredient? GetById(string id) =>
        string.IsNullOrWhiteSpace(id)
            ? null
            : _ingredients.FirstOrDefault(i => string.Equals(i.Id, id.Trim(), StringComparison.Ordinal));
}
