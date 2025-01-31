namespace Core.Domain.Entities;

public partial class Lost
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

    public double? Quantity { get; set; }

    public virtual Move IdNavigation { get; set; } = null!;
}
