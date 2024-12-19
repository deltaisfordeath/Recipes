using Microsoft.AspNetCore.Mvc;

namespace Recipes.Models;

public class RecipeViewModel
{
    public RecipeViewModel(){}
    public RecipeViewModel (Recipe recipe)
    {
        Name = recipe.Name;
        Description = recipe.Description;
        CookingTime = recipe.CookingTime;
        PrepTime = recipe.PrepTime;
        Servings = recipe.Servings;
        Difficulty = recipe.Difficulty;
        ImageUrl = recipe.ImageUrl;
        Ingredients = recipe.Ingredients;
        Instructions = recipe.Instructions;
        CreatedBy = recipe.CreatedBy;
    }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? CreatedBy { get; set; }
    public int? CookingTime { get; set; }
    public int? PrepTime { get; set; }
    public int? Servings { get; set; }
    public int? Difficulty { get; set; }
    public string? ImageUrl { get; set; }
    public List<Ingredient>? Ingredients { get; set; }
    public List<RecipeInstruction>? Instructions { get; set; }
}