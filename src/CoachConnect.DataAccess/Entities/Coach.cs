using System.ComponentModel.DataAnnotations;

namespace CoachConnect.DataAccess.Entities;

public class Coach
{
    [Key]
    public int Id { get; set; }

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

    [Required]
    public DateTime Created { get; init; }

    [Required]
    public DateTime Updated { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
