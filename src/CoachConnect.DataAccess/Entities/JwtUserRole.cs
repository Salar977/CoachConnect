using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key] // 2 [Key] annotations her så slipper ha PK column i tablellen. Den kombinerer og bruker begge samlet som en pk.
    [Column(Order = 1)]
    public Guid UserId { get; set; }

    [Key]
    [Column(Order = 2)]
    public int RoleId { get; set; }
}