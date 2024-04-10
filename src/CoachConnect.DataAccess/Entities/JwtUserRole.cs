using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key] // 2 [Key] annotations her så slipper ha PK column i tablellen. Den kombinerer og bruker begge samlet som en pk. Greit å kunne. Mer sikkert og.
    [Column(Order = 1)]
    public Guid UserId { get; set; } // brukt username istedenfor id pga vi har både type userid og coachid, vi skulle egentlig bare hatt userid på alt men det har vi ikke

    [Key]
    [Column(Order = 2)]
    public int RoleId { get; set; }
}

