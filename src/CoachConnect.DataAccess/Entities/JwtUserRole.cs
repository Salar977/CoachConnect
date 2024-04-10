using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    public string UserName { get; set; } = string.Empty; // brukt username istedenfor id pga vbi har både type userid og coachid, vi skulle egentlig bare hatt userid på alt men det har vi ikke
    public int RoleId { get; set; }   
}