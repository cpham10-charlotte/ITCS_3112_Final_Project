using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Contracts;

public interface IRecipeRepository
{
    public List<Recipe> GetAll();
    public Recipe GetById(string id);
    public void Add(Recipe recipe);
}