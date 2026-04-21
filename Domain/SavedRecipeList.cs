namespace ITCS_3112_Final_Project.Domain;

public class SavedRecipeList
{
    private string _userId;
    private List<Recipe> _recipes = new List<Recipe>();
    
    public  SavedRecipeList(string userId)
    {
        _userId = userId;
    }
}