namespace Core.Domain.Entities;

public partial class Turn
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public virtual ICollection<Difference> Differences { get; set; } = new List<Difference>();

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Money> Money { get; set; } = new List<Money>();

    public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
}
