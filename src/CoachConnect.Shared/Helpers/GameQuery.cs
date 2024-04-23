using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.Shared.Helpers;
public class GameQuery
{    
    public string? Location { get; set; } = null;
    public Guid? TeamId { get; set; }
    public DateTime? GameTime { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
