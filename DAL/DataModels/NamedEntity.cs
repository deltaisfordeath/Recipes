using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public abstract class NamedEntity : EntityBase
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }
}