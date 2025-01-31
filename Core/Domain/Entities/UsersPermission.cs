namespace Core.Domain.Entities;

public partial class UsersPermission
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdPermission { get; set; }

    public bool Granted { get; set; }

    public bool RequestElevation { get; set; }

    public virtual Permission IdPermissionNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
