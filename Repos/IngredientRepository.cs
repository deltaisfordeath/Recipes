using Microsoft.EntityFrameworkCore;
using Recipes.DAL;
using Recipes.Models;

namespace Recipes;

public class IngredientRepository(RecipeContext context) : NamedEntityRepository<RecipeContext, Ingredient>(context)
{
    public async Task<List<Ingredient>> FindLikeByName(string nameSubstring)
    {
        return await DbSet.Where(_ => _.Name.Contains(nameSubstring)).ToListAsync();
    }
}