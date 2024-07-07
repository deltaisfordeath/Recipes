using System.ComponentModel.DataAnnotations;

namespace Recipes.Models;

public abstract class NamedEntity : EntityBase
{
    public NamedEntity()
    {
        Name = "";
    }

    [Required]
    [MaxLength(60)]
    public string Name { get; set; }
}