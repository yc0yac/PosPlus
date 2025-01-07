using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Contracts.Entities;

namespace Core.Domain.Entities;

public class Entity : IEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
}