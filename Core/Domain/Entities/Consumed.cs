namespace Core.Domain.Entities;

public partial class Consumed
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

    public double? Quantity { get; set; }

    public int IdEntry { get; set; }

    public virtual Entry IdEntryNavigation { get; set; } = null!;
}
