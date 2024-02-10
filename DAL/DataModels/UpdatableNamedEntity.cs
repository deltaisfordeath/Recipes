using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public abstract class UpdatableNamedEntity : NamedEntity
{
    public DateTime UpdatedAt { get; set; }
}