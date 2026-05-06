using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Enums;

namespace ITCS_3112_Final_Project.Services;

public class RecipeFileLoader : IFileLoader
{
    private readonly IRecipeRepository _recipeRepo;
    private readonly IIngredientRepository _ingredientRepo;

    public RecipeFileLoader(IRecipeRepository recipeRepo, IIngredientRepository ingredientRepo)
    {
        _recipeRepo = recipeRepo;
        _ingredientRepo = ingredientRepo;
    }

    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var lines = File.ReadAllLines(filePath);
        
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];

            var sections = line.Split('|');

            if (sections.Length != 4)
                continue;

            string id = sections[0];
            string name = sections[1];
            VeganEnum vegan = Enum.Parse<VeganEnum>(sections[2]);

            string ingredientStrings = sections[3];

            List<Ingredient> ingredients = new();

            var ingredientParts = ingredientStrings.Split(',');

            foreach (var ing in ingredientParts)
            {
                var fields = ing.Split(':');

                if (fields.Length < 3)
                    continue;

                string ingId = fields[0];
                string ingName = fields[1];
                VeganEnum ingVegan = Enum.Parse<VeganEnum>(fields[2]);

                var existing = _ingredientRepo.GetById(ingId);

                if (existing != null)
                {
                    ingredients.Add(existing);
                }
                else
                {
                    var ingredient = new Ingredient(ingId, ingName, ingVegan);
                    _ingredientRepo.Add(ingredient);
                    ingredients.Add(ingredient);
                }
            }

            var recipe = new Recipe(name, id, vegan, ingredients);

            _recipeRepo.Add(recipe);
        }
    }
}