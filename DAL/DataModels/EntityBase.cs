using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public abstract class EntityBase
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}