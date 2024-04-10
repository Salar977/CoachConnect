using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    [Key]
    public string UserName { get; set; } = string.Empty;

    [ForeignKey(nameof(JwtRoleId))]
    public int JwtRoleId { get; set; }
}