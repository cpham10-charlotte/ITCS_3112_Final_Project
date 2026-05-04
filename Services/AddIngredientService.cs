using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Services;

public class AddIngredientService : PantryService
{
    private readonly IPantryRepository _pantryRepo;
    private readonly IIngredientRepository _ingredientRepo;

    public AddIngredientService(IPantryRepository pantryRepo, IIngredientRepository ingredientRepo)
    {
        _pantryRepo = pantryRepo;
        _ingredientRepo = ingredientRepo;
    }

    public override IReadOnlyList<Ingredient> ViewPantry(string userId)
    {
        throw new NotImplementedException();
    }

    public override void UpdatePantry(string userId, string ingredientId)
    {
        throw new NotImplementedException();
    }
}