
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
    public JwtUserRoleId Id { get; set; }

    //[ForeignKey(nameof(UserId))]
    public UserId UserId { get; set; }

    //[ForeignKey(nameof(CoachId))]
    public CoachId CoachId { get; set; }
    
    [ForeignKey(nameof(JwtRoleId))]
    public int JwtRoleId { get; set; } 
}