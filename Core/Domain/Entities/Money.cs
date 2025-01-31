namespace Core.Domain.Entities;

public partial class Money
{
    public int Id { get; set; }

    public int IdTurn { get; set; }

    public double Amount { get; set; }

    public string? Description { get; set; }

    public int? IdMove { get; set; }

    public virtual Move? IdMoveNavigation { get; set; }

    public virtual Turn IdTurnNavigation { get; set; } = null!;
}
