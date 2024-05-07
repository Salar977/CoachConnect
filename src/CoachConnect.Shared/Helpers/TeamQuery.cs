namespace CoachConnect.Shared.Helpers;
public class TeamQuery
{
    public string TeamCity { get; set; } = string.Empty;

    public string TeamName { get; set; } = string.Empty;

    public DateTime TeamTime { get; init; }

    public DateTime Updated { get; set; }
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
