namespace Core.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int IdCategory { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public int? Composed { get; set; }

    public int? Unitary { get; set; }

    public string? Um { get; set; }

    public virtual ICollection<Difference> Differences { get; set; } = new List<Difference>();

    public virtual ICollection<Existence> Existences { get; set; } = new List<Existence>();

    public virtual Category IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<Ingredient> IngredientIdProductIngredientNavigations { get; set; } = new List<Ingredient>();

    public virtual ICollection<Ingredient> IngredientIdProductMainNavigations { get; set; } = new List<Ingredient>();

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
