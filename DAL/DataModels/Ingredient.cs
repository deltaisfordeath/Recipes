using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public class Ingredient : NamedEntity
{
    public string? Description { get; set; }
}