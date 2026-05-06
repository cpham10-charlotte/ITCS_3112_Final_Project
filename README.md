## 1. Project Overview 
    
This project is an ingredient tracking service that provides recipes based off of the ingredients in your pantry. It 
provides the ability to add and delete ingredients to a virtual pantry. Then, using the ingredients in your virtual 
pantry, it suggests recipes to you. The project also provides the ability to add new recipes.

## 2. Build & Run Instructions

To run this project, you need to download the repository. After downloading the repository, you open the project in
your chosen IDE and press the IDE's run button.

## 3. Required OOP Features

| OOP Feature               | File Name               | Line Numbers | Reasoning/Purpose                                                                                                                               |
|---------------------------|-------------------------|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| Inheritance               | AddIngredientService    | 17 - 32      | Separates the responsibility for adding ingredients from the inherited Pantry service                                                           |
| Inheritance               | DeleteIngredientService | 17 - 33      | Separates the responsibility for deleting ingredients from the inherited Pantry service                                                         |
| Interface                 | IFileLoader             | ALL          | Prevents interaction with FileLoader to mitigate errors from dependencies                                                                       |
| Interface                 | IRecipeCreationService  | ALL          | Prevents interaction with RecipeCreationService to mitigate errors from dependencies                                                            |
| Interface                 | IRecommendationService  | ALL          | Prevents interaction with RecommendationService to mitigate errors from dependencies                                                            | 
| Polymorphism              | AddIngredientService    | 23 - 32      | Allows the AddIngredientService to function as a PantryService while still performing its expected behavior                                     |
| Polymorphism              | DeleteIngredientService | 23 - 33      | Allows the DeleteIngredientService to function as a PantryService while still performing its expected behavior                                  |
| Enum                      | VeganEnum               | ALL          | Determines whether or not a recipe is vegan or not for dietary restrictions                                                                     |
| Data Structure | RecipeRepository | 8 | Lists are used to store objects in all of our repositories because they are easily accessed and mutated                                         |
| Console I/O | Program | 67 - 250 | User Input is needed to login to the service, as well as interact with the pantry and recipes within the service | 

## 4. Design Patterns
| Pattern   | Category   | File Name | Line Numbers | Rationale                                                                                                                                       |
|-----------|------------| --- | --- |-------------------------------------------------------------------------------------------------------------------------------------------------|
| Singleton | Behavioral | RecipeFileLoader        | ALL          | RecipeFileLoader only needs one instance to provide file loading to all classes within the code                                                 |
| Factory   | Creational | RecipeCreationService | ALL          | Recipe creation needs to follow a certain template but the actual creation needs to be abstracted since we allow users to add their own recipes |
| Strategy | Behavioral | AddIngredientService | 17 - 32 | Both AddIngredientService and DeleteIngredientService inherit PantryService and can be used in place of PantryService. This allows us to switch between adding and deleting ingredients smoothly during runtime. |
## 5. Design Decisions

In our design, we coupled the services and repositories based off of the domain they used. For example, the 
AuthenticationService mainly uses the User domain. This domain-based grouping is easily visible in our UML. 