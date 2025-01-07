using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class UserPermission : Entity
{
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("area")]
    public string Area { get; set; }
    
    [Column("id_user")]
    public int IdUser { get; set; }
    
    [Column("granted")]
    public bool Granted { get; set; }
    
    [Column("request_elevation")]
    public bool RequestElevation { get; set; }
}