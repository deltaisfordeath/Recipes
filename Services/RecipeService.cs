


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipes.Helpers;
using Recipes.Models;
using Recipes.Repos;

namespace Recipes.Services;

public class RecipeService(
    RecipeRepository recipeRepository,
    IngredientRepository ingredientRepository,
    UserManager<RecipeUser> userManager
    )
{
    public async Task AddRecipe(Recipe recipe, List<Ingredient> ingredients, List<RecipeInstruction> instructions, string CreatedBy)
    {
        await recipeRepository.AddRecipe(recipe);

        foreach (var ingredient in ingredients)
        {
            var existing = await ingredientRepository.FindByName(ingredient.Name);

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
                ingredient.CreatedBy = CreatedBy;
                ingredient.CreatedAt = DateTime.Now;
                ingredient.Description = ingredient.Description ?? "A New Ingredient";
                await ingredientRepository.AddAsync(ingredient);
                recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient
                };
            }

            await recipeRepository.AddRecipeIngredient(recipeIngredient);
        }

        for (var i = 0; i < instructions.Count; i++)
        {
            instructions[i].Recipe = recipe;
            await recipeRepository.AddRecipeInstruction(instructions[i]);
        }

        await recipeRepository.SaveChangesAsync();
    }

    public async Task<PaginatedList<Recipe>> GetPageOfRecipes(int pageNum)
    {
        var recipes = await recipeRepository.GetPageOfRecipes(pageNum);
        return recipes;
    }

    public List<Recipe> GetAllRecipes()
    {
        var recipes = recipeRepository.GetAllRecipes();
        return recipes;
    }

    public async Task<Recipe?> GetFullRecipe(int id)
    {
        var recipe = await recipeRepository.FindById(id);
        if (recipe == null) return null;

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

        var ingredients = await recipeRepository.GetRecipeIngredients(recipe.Id);

        fullRecipe.Ingredients = ingredients;

        var instructions = await recipeRepository.GetRecipeInstructions(recipe.Id);

        fullRecipe.Instructions = instructions;

        var recipeCreator = await userManager.Users.FirstOrDefaultAsync(user => user.Id == recipe.CreatedBy);
        var creatorName = recipeCreator?.UserName ?? "Anonymous";
        fullRecipe.CreatedBy = creatorName;

        return fullRecipe;
    }

    public async Task<List<Ingredient>> GetIngredientSuggestions(string nameSubstring)
    {
        return await ingredientRepository.FindLikeByName(nameSubstring);
    }
}