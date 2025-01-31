namespace Core.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? Photo { get; set; }

    public bool Disabled { get; set; }

    public string? Email { get; set; }

    public string? Salary { get; set; }

    public string? Position { get; set; }

    public bool Isadmin { get; set; }

    public string? SharedPassword { get; set; }

    public virtual ICollection<Move> Moves { get; set; } = new List<Move>();

    public virtual ICollection<Turn> Turns { get; set; } = new List<Turn>();

    public virtual ICollection<UsersPermission> UsersPermissions { get; set; } = new List<UsersPermission>();
    
    public string PermissionGrantedString() => GetPermissionString(p => p.Granted);

    public string PermissionWithElevationString() => GetPermissionString(p => p.RequestElevation);

    private string GetPermissionString(Func<UsersPermission, bool> predicate)
    {
        if (UsersPermissions?.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(',', UsersPermissions?
            .Where(predicate)
            .Select(p => p.IdPermissionNavigation?.Name) ?? []);
    }
}
