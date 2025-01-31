namespace Core.Domain.Entities;

public partial class Owner
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Message { get; set; }

    public byte[]? Logo { get; set; }
}
