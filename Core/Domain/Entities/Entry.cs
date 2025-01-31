namespace Core.Domain.Entities;

public partial class Entry
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

    public double? Quantity { get; set; }

    public int? IdMove { get; set; }

    public virtual ICollection<Consumed> Consumeds { get; set; } = new List<Consumed>();

    public virtual Move? IdMoveNavigation { get; set; }
}
