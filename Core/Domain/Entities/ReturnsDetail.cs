namespace Core.Domain.Entities;

public partial class ReturnsDetail
{
    public int Id { get; set; }

    public int IdSaleDetail { get; set; }

    public int IdReturn { get; set; }

    public int? Toexpenses { get; set; }

    public virtual Return IdReturnNavigation { get; set; } = null!;

    public virtual SalesDetail IdSaleDetailNavigation { get; set; } = null!;
}
