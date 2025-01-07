using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

[Table("Users")]
public sealed class User : Entity
{
    [Required, MinLength(5)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Required, MinLength(5)]
    [Column("username")]
    public string Username { get; set; } = null!;

    [Required, MinLength(8)]
    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("disabled")] public bool Disabled { get; set; }

    [Column("photo")] public string? Photo { get; set; }

    [Column("email"), MaxLength(64)] public string? Email { get; set; }

    [Column("salary"), MaxLength(256)] public string? Salary { get; set; }

    [Column("position"), MaxLength(512)] public string? Position { get; set; }

    [Column("isadmin")] public bool IsAdmin { get; set; }

    [Column("shared_password")] public string? SharedPassword { get; set; }

    [NotMapped] public List<UserPermission>? Permissions { get; set; }

    public string? PermissionGrantedString()
    {
        if (Permissions == null || Permissions.Count == 0) return string.Empty;
        return string.Join(',', Permissions.Where(p => p.Granted).Select(permission => permission.Name));
    }

    public string? PermissionWithElevationString()
    {
        if (Permissions == null || Permissions.Count == 0) return string.Empty;
        return string.Join(',', Permissions.Where(p => p.RequestElevation).Select(permission => permission.Name));
    }
   
}