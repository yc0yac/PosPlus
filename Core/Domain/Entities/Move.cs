namespace Core.Domain.Entities;

public partial class Move
{
    public int Id { get; set; }

    public string? Concept { get; set; }

    public string? Description { get; set; }

    public string? Date { get; set; }

    public int? IdUser { get; set; }

    public int? IdTurn { get; set; }

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    public virtual Turn? IdTurnNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual Lost? Lost { get; set; }

    public virtual ICollection<Money> Money { get; set; } = new List<Money>();

    public virtual Return? Return { get; set; }

    public virtual Sale? Sale { get; set; }
}
