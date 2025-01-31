namespace Core.Domain.Entities;

public partial class Return
{
    public int Id { get; set; }

    public int? IdSale { get; set; }

    public double? Paid { get; set; }

    public virtual Move IdNavigation { get; set; } = null!;

    public virtual Sale? IdSaleNavigation { get; set; }

    public virtual ICollection<ReturnsDetail> ReturnsDetails { get; set; } = new List<ReturnsDetail>();
}
