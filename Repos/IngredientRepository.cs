using Recipes.DAL;
using Recipes.Models;

namespace Recipes;

public class IngredientRepository(RecipeContext context) : NamedEntityRepository<RecipeContext, Ingredient>(context)
{
}