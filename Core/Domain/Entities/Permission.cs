namespace Core.Domain.Entities;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Area { get; set; } = null!;

    public virtual ICollection<UsersPermission> UsersPermissions { get; set; } = new List<UsersPermission>();
}
