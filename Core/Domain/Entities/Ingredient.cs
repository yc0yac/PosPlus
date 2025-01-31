namespace Core.Domain.Entities;

public partial class Ingredient
{
    public int Id { get; set; }

    public int IdProductMain { get; set; }

    public int IdProductIngredient { get; set; }

    public double? Quantity { get; set; }

    public virtual Product IdProductIngredientNavigation { get; set; } = null!;

    public virtual Product IdProductMainNavigation { get; set; } = null!;
}
