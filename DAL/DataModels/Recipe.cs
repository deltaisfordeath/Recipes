using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public class Recipe : UpdatableNamedEntity
{
    [MaxLength(1000)]
    public string? Description { get; set; }
    public int? CookingTime { get; set; }
    public int? PrepTime { get; set; }
    public int? Servings { get; set; }
    public int? Difficulty { get; set; }
    [MaxLength(255)]
    public string? ImageUrl { get; set; }
    [Required]
    public List<Ingredient> Ingredients = [];
    [Required]
    public List<RecipeInstruction> Instructions = [];
}