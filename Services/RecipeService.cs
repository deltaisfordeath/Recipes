


using Recipes.Models;

namespace Recipes.Services;

public class RecipeService(
    RecipeRepository recipeRepository,
    IngredientRepository ingredientRepository
    )
{
    private readonly RecipeRepository _recipeRepository = recipeRepository;
    private readonly IngredientRepository _ingredientRepository = ingredientRepository;


    public async Task AddRecipe(Recipe recipe, List<Ingredient> ingredients, List<RecipeInstruction> instructions)
    {
        await _recipeRepository.AddRecipe(recipe);

        foreach (var ingredient in ingredients)
        {
            var existing = await _ingredientRepository.FindByName(ingredient.Name);

            RecipeIngredient recipeIngredient;

            if (existing != null)
            {
                recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = existing
                };
            }
            else
            {
                await _ingredientRepository.AddAsync(ingredient);
                recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient
                };
            }

            await _recipeRepository.AddRecipeIngredient(recipeIngredient);
        }

        for (var i = 0; i < instructions.Count; i++)
        {
            instructions[i].Recipe = recipe;
            await _recipeRepository.AddRecipeInstruction(instructions[i]);
        }

        await _recipeRepository.SaveChangesAsync();
    }

    public List<Recipe> GetAllRecipes()
    {
        var recipes = _recipeRepository.GetAllRecipes();
        return recipes;
    }

    public async Task<Recipe> GetFullRecipe(int id)
    {
        var recipe = await _recipeRepository.FindById(id);
        if (recipe == null) return new Recipe();

        var fullRecipe = new Recipe
        {
            Name = recipe.Name,
            Description = recipe.Description,
            Servings = recipe.Servings,
            CookingTime = recipe.CookingTime,
            Difficulty = recipe.Difficulty,
            PrepTime = recipe.PrepTime,
            ImageUrl = recipe.ImageUrl
        };

        var ingredients = await _recipeRepository.GetRecipeIngredients(recipe.Id);

        fullRecipe.Ingredients = ingredients;

        var instructions = await _recipeRepository.GetRecipeInstructions(recipe.Id);

        fullRecipe.Instructions = instructions;

        return fullRecipe;
    }

    public async Task<List<Ingredient>> GetIngredientSuggestions(string nameSubstring)
    {
        return await _ingredientRepository.FindLikeByName(nameSubstring);
    }
}