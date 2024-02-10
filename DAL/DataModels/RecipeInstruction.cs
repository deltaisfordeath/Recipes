using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Recipes.Models;

[PrimaryKey(nameof(Recipe.Id), nameof(Step))]
public class RecipeInstruction
{
    [ForeignKey(nameof(Recipe.Id))]
    public Recipe? Recipe { get; set; }
    public int Step { get; set; }
    public string? Description { get; set; }

}