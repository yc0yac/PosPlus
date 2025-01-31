namespace Core.Domain.Entities;

public partial class Difference
{
    public int Id { get; set; }

    public int? IdTurn { get; set; }

    public int? IdProduct { get; set; }

    public double? Existence { get; set; }

    public double? Real { get; set; }

    public string? DiffDate { get; set; }

    public int? Fixed { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Turn? IdTurnNavigation { get; set; }
}
