using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CoachConnect.DataAccess.Entities.JwtRole;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key]
    public string UserName { get; set; } = string.Empty;

    [ForeignKey(nameof(Role))]
    public int JwtRoleId { get; set; }

    public JwtRole? Role { get; set; }  // Navigation property for JwtRole  
}