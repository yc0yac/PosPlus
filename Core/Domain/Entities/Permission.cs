using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

[Table("Permissions")]
public sealed class Permission : Entity
{
    [Column("name")]
    public required string Name { get; set;}
    [Column("description")]
    public required string Description { get; set;}
    [Column("area")]
    public required string Area { get; set;}
}