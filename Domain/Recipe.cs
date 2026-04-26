using ITCS_3112_Final_Project.Enums;

namespace ITCS_3112_Final_Project.Domain;

public class Recipe
{
    public string Name { get; }
    public string Id { get; }
    public VeganEnum Vegan { get; }
    public List<Ingredient> Ingredients { get; }

    public Recipe(string name, string id, VeganEnum vegan, List<Ingredient> ingredients)
    {
        Name = name;
        Id = id;
        Vegan = vegan;
        Ingredients = ingredients;
    }
}