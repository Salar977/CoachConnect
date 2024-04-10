using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public class JwtRole
{   
    [Key]
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

}