using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public readonly record struct JwtUserRoleId(Guid jwtUserRoleId)
<<<<<<< HEAD
{
    public static JwtUserRoleId NewId => new JwtUserRoleId(Guid.NewGuid());
    public static JwtUserRoleId Empty => new JwtUserRoleId(Guid.Empty);
};
public class JwtUserRole 
{
=======
{
    public static JwtUserRoleId NewId => new JwtUserRoleId(Guid.NewGuid());
    public static JwtUserRoleId Empty => new JwtUserRoleId(Guid.Empty);
};
public class JwtUserRole 
{
>>>>>>> main
    [Key]
    public JwtUserRoleId Id { get; set; }

    public Guid UserOrCoachId { get; set; }
<<<<<<< HEAD

    [ForeignKey(nameof(RoleId))]
=======
       
>>>>>>> main
    public int RoleId { get; set; }

    public JwtRole? Role { get; set; }
}