using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
