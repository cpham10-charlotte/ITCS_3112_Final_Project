using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Services;

public sealed class PantryService : IPantryService
{
    private readonly IPantryRepository _pantryRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public PantryService(IPantryRepository pantryRepository, IIngredientRepository ingredientRepository)
    {
        _pantryRepository = pantryRepository ?? throw new ArgumentNullException(nameof(pantryRepository));
        _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
    }

    public IReadOnlyList<Ingredient> ViewPantry(string userId)
    {
        var pantry = GetOrCreatePantry(userId);
        return pantry.GetIngredients();
    }

    public bool AddIngredient(string userId, string ingredientId)
    {
        var pantry = GetOrCreatePantry(userId);
        var ingredient = _ingredientRepository.GetById(ingredientId);
        if (ingredient is null)
        {
            return false;
        }

        var added = pantry.AddIngredient(ingredient);
        if (added)
        {
            _pantryRepository.Save(pantry);
        }

        return added;
    }

    public bool RemoveIngredient(string userId, string ingredientId)
    {
        var pantry = GetOrCreatePantry(userId);
        var ingredient = _ingredientRepository.GetById(ingredientId);
        if (ingredient is null)
        {
            return false;
        }

        var removed = pantry.RemoveIngredient(ingredient);
        if (removed)
        {
            _pantryRepository.Save(pantry);
        }

        return removed;
    }

    private Pantry GetOrCreatePantry(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id cannot be null or whitespace.", nameof(userId));
        }

        var trimmedUserId = userId.Trim();
        var pantry = _pantryRepository.GetByUserId(trimmedUserId);
        if (pantry is not null)
        {
            return pantry;
        }

        pantry = new Pantry(trimmedUserId);
        _pantryRepository.Save(pantry);
        return pantry;
    }
}

