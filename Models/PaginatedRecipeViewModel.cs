namespace Recipes.Models;

public class PaginatedRecipeViewModel
{
    public List<Recipe> items { get; set; }
    public bool hasNextPage { get; set; }
}