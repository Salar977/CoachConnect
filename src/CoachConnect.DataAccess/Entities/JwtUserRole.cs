using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key]
    [Column(Order = 1)]
    public string? UserName { get; set; } // brukt username istedenfor id pga vi har både type userid og coachid, vi skulle egentlig bare hatt userid på alt men det har vi ikke

    [Key]
    [Column(Order = 2)]
    public int RoleId { get; set; }
}

//    public string UserName { get; set; } = string.Empty; 
//    public int RoleId { get; set; }   
//}