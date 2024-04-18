using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.Shared.Helpers;
public class GameQuery
{
    public string? Location { get; set; } = null;
    public string? HomeTeam { get; set; } = null;
    public string? AwayTeam { get; set; } = null;
    public DateTime? GameDate { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
