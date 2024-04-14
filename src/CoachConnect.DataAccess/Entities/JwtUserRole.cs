
using Mysqlx.Crud;
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

    public string? UserName { get; set; }
    
    public int JwtRoleId { get; set; }


    //public virtual ICollection<User> Users { get; set; } = new List<User>();
    //public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();
    //public virtual Coach? Coach { get; set; }
    //public virtual User? User { get; set; }
}