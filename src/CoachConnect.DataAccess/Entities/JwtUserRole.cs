using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key] 
    [Column(Order = 1)]
    public Guid UserOrCoachId { get; set; }

    [Key]
    [Column(Order = 2)]
    public int RoleId { get; set; }
}