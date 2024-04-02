namespace CoachConnect.Shared.Helpers;

public class PracticeAttendanceQuery
{
    public Guid? PracticeId { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

}
