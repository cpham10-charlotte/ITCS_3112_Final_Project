namespace ITCS_3112_Final_Project.Domain;

public class Recipe
{
    public string Name { get; }
    public string Id { get; }
    public VeganEnum Vegan { get; }

    public Recipe(string name, string id, VeganEnum vegan)
    {
        Name = name;
        Id = id;
        Vegan = vegan;
    }
}