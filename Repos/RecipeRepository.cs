using Microsoft.EntityFrameworkCore;
using Recipes.DAL;
using Recipes.Helpers;
using Recipes.Models;

namespace Recipes.Repos;

public class RecipeRepository(RecipeContext context) : NamedEntityRepository<RecipeContext, Recipe>(context)
{

    private readonly RecipeContext _recipeContext = context;

    public async Task AddRecipe(Recipe recipe)
    {
        await _recipeContext.Recipes.AddAsync(recipe);
    }

    public async Task AddRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        await _recipeContext.RecipeIngredients.AddAsync(recipeIngredient);
    }

    public async Task AddRecipeInstruction(RecipeInstruction recipeInstruction)
    {
        await _recipeContext.RecipeInstructions.AddAsync(recipeInstruction);
    }

    public async Task<List<Ingredient>> GetRecipeIngredients(int recipeId)
    {
        var ingredients = await _recipeContext.Ingredients
            .Where(ing => _recipeContext.RecipeIngredients
            .Any(ri => ri.Recipe.Id == recipeId && ri.Ingredient.Id == ing.Id))
            .ToListAsync();

        return ingredients;
    }

    public async Task<List<RecipeInstruction>> GetRecipeInstructions(int recipeId)
    {
        var instructions = await _recipeContext.RecipeInstructions
            .Where(_ => _.Recipe.Id == recipeId)
            .Select(_ => new RecipeInstruction {Step = _.Step, Description = _.Description}).ToListAsync();
        return instructions;
    }

    public async Task<PaginatedList<Recipe>> GetPageOfRecipes(int pageNum)
    {
        return await PaginatedList<Recipe>.CreateAsync(_recipeContext.Recipes, pageNum);
    }

    public List<Recipe> GetAllRecipes()
    {
        return [.. _recipeContext.Set<Recipe>()];
    }

    public async Task SaveChangesAsync()
    {
        await _recipeContext.SaveChangesAsync();
    }
}