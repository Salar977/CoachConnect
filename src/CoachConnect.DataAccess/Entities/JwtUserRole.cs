
using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct JwtUserRoleId(Guid jwtUserRoleId)
{
    public static JwtUserRoleId NewId => new JwtUserRoleId(Guid.NewGuid());
    public static JwtUserRoleId Empty => new JwtUserRoleId(Guid.Empty);
};

public class JwtUserRole
{
    [Key]
    [Column(Order = 1)]
    public JwtUserRoleId Id { get; set; }

    [Key]
    [Column(Order = 2)]
    public string? UserName { get; set; }
    
    [ForeignKey(nameof(JwtRole.Id))]
    public int JwtRoleId { get; set; } 

    public virtual User? User { get; set; }
    public virtual Coach? Coach { get; set; }
}