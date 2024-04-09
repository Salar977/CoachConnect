namespace CoachConnect.DataAccess.Entities;

public class JwtUserRole
{
    // public string UserName { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}