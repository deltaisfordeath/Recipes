using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.DAL
{
    public class RecipeContext(DbContextOptions<RecipeContext> contextOptions) : DbContext(contextOptions) 
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeInstruction> RecipeInstructions { get; set; }

    }
}