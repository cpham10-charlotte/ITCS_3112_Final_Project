using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;
using ITCS_3112_Final_Project.Enums;
using ITCS_3112_Final_Project.Repositories;
using ITCS_3112_Final_Project.Services;

namespace ITCS_3112_Final_Project;

class Program
{
    static void Main(string[] args)
    {
        //repos
        IRecipeRepository recipeRepo = new RecipeRepository();
        IIngredientRepository ingredientRepo = new IngredientRepository();
        IPantryRepository pantryRepo = new PantryRepository();
        IUserRepository userRepo = new UserRepository();
        
        //load the text files at runtime to populate repositories
        string basePath = AppContext.BaseDirectory;
        string userFilePath = Path.Combine(basePath, "Docs", "users.txt");
        string recipeFilePath = Path.Combine(basePath, "Docs", "recipes.txt");
        IFileLoader userLoader = new UserFileLoader(userRepo);
        userLoader.Load(userFilePath); //change path to whichever yours is for this file
        IFileLoader recipeLoader = new RecipeFileLoader(recipeRepo, ingredientRepo);
        recipeLoader.Load(recipeFilePath); //change path to whichever yours is for this file

        //services
        IAccountService authService = new AuthenticationService(userRepo);
        PantryService addService = new AddIngredientService(pantryRepo, ingredientRepo);
        PantryService removeService = new RemoveIngredientService(pantryRepo, ingredientRepo);
        IRecipeCreationService recipeService = new RecipeCreationService(recipeRepo, ingredientRepo);
        IRecommendationService recommendationService = new RecommendationService(recipeRepo, ingredientRepo, pantryRepo);
        
        Console.WriteLine("Welcome to the Recipe Recommendation System!");

        RunMenu(authService, addService, removeService, recipeService, recommendationService, recipeRepo, ingredientRepo);
    }

    static void RunMenu(
        IAccountService authService,
        PantryService addService,
        PantryService removeService,
        IRecipeCreationService recipeService,
        IRecommendationService recommendationService,
        IRecipeRepository recipeRepo,
        IIngredientRepository ingredientRepo)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("Please select an option below");

            if (authService.IsLoggedIn())
                Console.WriteLine($"Logged in as: {authService.GetCurrentUser()!.name} (ID: {authService.GetCurrentUser()!.id})");
            else
                Console.WriteLine("Not logged in.");

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Logout");
            Console.WriteLine("3. View Pantry");
            Console.WriteLine("4. Add Ingredient");
            Console.WriteLine("5. Remove Ingredient");
            Console.WriteLine("6. View Recommendations");
            Console.WriteLine("7. View Recipes"); 
            Console.WriteLine("8. Create Recipe");
            Console.WriteLine("9. Exit");        

            Console.Write("Choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Write("Enter User ID: ");
                    string loginId = Console.ReadLine() ?? "";

                    try
                    {
                        authService.Login(loginId);
                        Console.WriteLine($"Welcome {authService.GetCurrentUser()!.name}!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Login failed: " + ex.Message);
                    }
                    break;
                
                case "2":
                    if (authService.IsLoggedIn())
                    {
                        string name = authService.GetCurrentUser()!.name;
                        authService.Logout();
                        Console.WriteLine($"You have been logged out {name}!");
                    }
                    else
                    {
                        Console.WriteLine("You are not logged in.");
                    }
                    break;
                
                case "3":
                    if (!authService.IsLoggedIn())
                    {
                        Console.WriteLine("Please login first.");
                        break;
                    }

                    var pantry = addService.ViewPantry(authService.GetCurrentUser()!.id);

                    foreach (var i in pantry)
                        Console.WriteLine($"{i.Id} - {i.Name}");

                    break;
                
                case "4":
                    if (!authService.IsLoggedIn())
                    {
                        Console.WriteLine("Please login first.");
                        break;
                    }
                    Console.WriteLine("Available Ingredients:");
                    foreach (var r in recipeRepo.GetAll())
                    {
                        foreach (var ingredient in r.Ingredients)
                        {
                            Console.WriteLine($"{ingredient.Id} - {ingredient.Name} ({ingredient.VeganStatus})");
                        }
                    }

                    Console.Write("Ingredient ID: ");
                    string ingredientIdToAdd = Console.ReadLine()!;

                    addService.UpdatePantry(authService.GetCurrentUser()!.id, ingredientIdToAdd);
                    Console.WriteLine("Added.");
                    break;
                
                case "5":
                    if (!authService.IsLoggedIn())
                    {
                        Console.WriteLine("Please login first.");
                        break;
                    }

                    Console.Write("Ingredient ID: ");
                    string ingredientIdToRemove = Console.ReadLine()!;

                    removeService.UpdatePantry(authService.GetCurrentUser()!.id, ingredientIdToRemove);
                    Console.WriteLine("Removed.");
                    break;
                
                case "6":
                    if (!authService.IsLoggedIn())
                    {
                        Console.WriteLine("Please login first.");
                        break;
                    }

                    var recs = recommendationService.Recommend(authService.GetCurrentUser()!.id);

                    foreach (var r in recs)
                        Console.WriteLine($"{r.Id} - {r.Name}");

                    break;
                
                case "7":
                    var recipes = recipeRepo.GetAll();

                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("No recipes found.");
                        break;
                    }

                    foreach (var r in recipes)
                    {
                        Console.WriteLine($"{r.Id} - {r.Name} ({r.Vegan})");

                        foreach (var ing in r.Ingredients)
                        {
                            Console.WriteLine($"   {ing.Id} - {ing.Name} ({ing.VeganStatus})");
                        }

                        Console.WriteLine();
                    }

                    break;
                
                
                case "8":
                    if (!authService.IsLoggedIn())
                    {
                        Console.WriteLine("Please login first.");
                        break;
                    }
                    
                    string blankRecipeId =  "";

                    Console.Write("Recipe Name: ");
                    string recipeName = Console.ReadLine() ?? "";
                    Console.WriteLine("Available Ingredients:");
                    HashSet<string> seen = new();

                    foreach (var r in recipeRepo.GetAll())
                    {
                        foreach (var ing in r.Ingredients)
                        {
                            if (seen.Add(ing.Id))
                            {
                                Console.WriteLine($"{ing.Id} - {ing.Name} ({ing.VeganStatus})");
                            }
                        }
                    }
                    Console.WriteLine("Enter ingredient IDs separated by commas (example: 1,2,3): ");
                    string ingInput = Console.ReadLine() ?? "";

                    List<Ingredient> ingredients = new();

                    foreach (var id in ingInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var ingredient = ingredientRepo.GetById(id.Trim());
                        if (ingredient != null)
                        {
                            ingredients.Add(ingredient);
                        }
                    }
                    VeganEnum vegan = ingredients.All(i => i.VeganStatus == VeganEnum.VEGAN)
                        ? VeganEnum.VEGAN
                        : VeganEnum.NON_VEGAN;

                    recipeService.CreateRecipe(
                        authService.GetCurrentUser()!,
                        recipeName,
                        blankRecipeId,
                        vegan,
                        ingredients
                    );

                    Console.WriteLine("Recipe created.");
                    break;
 
                case "9":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }
    }
}