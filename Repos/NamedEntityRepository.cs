using Recipes.Models;
using Microsoft.EntityFrameworkCore;
using Recipes.DAL;


namespace Recipes;

public class NamedEntityRepository<TContext, TEntity>(RecipeContext context)
    : GenericRepository<RecipeContext, TEntity>(context)
    where TContext : DbContext
    where TEntity : NamedEntity
{


    public async Task<TEntity?> FindByName(string name)
    {
        return await DbSet.Where(_ => _.Name == name).FirstOrDefaultAsync();
    }

}