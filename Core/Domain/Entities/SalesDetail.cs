namespace Core.Domain.Entities;

public partial class SalesDetail
{
    public int Id { get; set; }

    public int IdProduct { get; set; }

    public double? FinalPrice { get; set; }

    public double? PurchasePrice { get; set; }

    public double? SalePrice { get; set; }

    public double? Quantity { get; set; }

    public int? IdSale { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Sale? IdSaleNavigation { get; set; }

    public virtual ICollection<ReturnsDetail> ReturnsDetails { get; set; } = new List<ReturnsDetail>();
}
