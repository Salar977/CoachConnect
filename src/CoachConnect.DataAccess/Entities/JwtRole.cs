using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public class JwtRole
{   
    [Key]
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
}