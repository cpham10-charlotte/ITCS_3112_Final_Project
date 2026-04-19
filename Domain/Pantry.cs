namespace ITCS_3112_Final_Project.Domain;

/// <summary>
/// Holds the ingredients available to a specific user.
/// </summary>
public sealed class Pantry
{
    private readonly List<Ingredient> _ingredients = new();

    public Pantry(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id cannot be null or whitespace.", nameof(userId));
        }

        UserId = userId.Trim();
    }

    public string UserId { get; }

    /// <summary>
    /// Returns a snapshot of the pantry contents.
    /// </summary>
    public IReadOnlyList<Ingredient> GetIngredients() => _ingredients.ToList();

    /// <summary>
    /// Adds an ingredient if it is not already present (by equality rules on <see cref="Ingredient"/>).
    /// </summary>
    public bool AddIngredient(Ingredient ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);

        if (_ingredients.Contains(ingredient))
        {
            return false;
        }

        _ingredients.Add(ingredient);
        return true;
    }

    /// <summary>
    /// Removes an ingredient when a matching instance exists in the pantry.
    /// </summary>
    public bool RemoveIngredient(Ingredient ingredient)
    {
        ArgumentNullException.ThrowIfNull(ingredient);
        return _ingredients.Remove(ingredient);
    }
}
