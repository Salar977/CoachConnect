using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct JwtUserRoleId(Guid jwtUserRoleId)
{
    public static JwtUserRoleId NewId => new JwtUserRoleId(Guid.NewGuid());
    public static JwtUserRoleId Empty => new JwtUserRoleId(Guid.Empty);
};
public class JwtUserRole 
{
<<<<<<< HEAD
=======
    [Key] 
    [Column(Order = 1)]
    public Guid UserOrCoachId { get; set; }

>>>>>>> main
    [Key]
    public JwtUserRoleId Id { get; set; }

    public Guid UserOrCoachId { get; set; }
       
    public int RoleId { get; set; }
}