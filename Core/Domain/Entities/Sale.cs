namespace Core.Domain.Entities;

public partial class Sale
{
    public int Id { get; set; }

    public int? IdClient { get; set; }

    public string Invoice { get; set; } = null!;

    public int Paid { get; set; }

    public string? DatePaid { get; set; }

    public string? DatePaidLimit { get; set; }

    public string? PaidMethod { get; set; }

    public string? Reference { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Move IdNavigation { get; set; } = null!;

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
