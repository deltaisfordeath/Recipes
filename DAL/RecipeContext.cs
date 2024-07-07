using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.DAL
{
    public class RecipeContext(DbContextOptions<RecipeContext> contextOptions) : IdentityDbContext<RecipeUser>(contextOptions) 
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeInstruction> RecipeInstructions { get; set; }

    }
}