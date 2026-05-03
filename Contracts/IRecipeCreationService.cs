using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Enums;

namespace ITCS_3112_Final_Project.Contracts;

public interface IRecipeCreationService
{
    Recipe CreateRecipe(User user, string name, string id, VeganEnum vegan, List<Ingredient> ingredients);
}