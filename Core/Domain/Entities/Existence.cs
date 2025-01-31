namespace Core.Domain.Entities;

public partial class Existence
{
    public int Id { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

    public double? SaleStock { get; set; }

    public double? StoreStock { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
