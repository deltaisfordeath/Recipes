using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Recipes.Models;

[PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
public class RecipeIngredient {

    [ForeignKey(nameof(RecipeId))]
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }

    [ForeignKey(nameof(IngredientId))]
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }

}