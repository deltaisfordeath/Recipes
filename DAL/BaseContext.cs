using Microsoft.EntityFrameworkCore;
using Recipes.Models;

namespace Recipes.DAL;

public class BaseContext(DbContext context)
{
    public readonly DbContext _context = context;
    
}