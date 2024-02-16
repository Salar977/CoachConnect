using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MinLength(5), MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(2), MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(2), MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string HashedPassword { get; set; } = string.Empty;

    [Required]
    public string Salt { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
