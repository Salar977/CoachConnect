using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;

public class PracticeAttendanceRepository : IPracticeAttendanceRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<PracticeAttendanceRepository> _logger;

    public PracticeAttendanceRepository(CoachConnectDbContext dbContext,
                                        ILogger<PracticeAttendanceRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<PracticeAttendance?> DeleteByIdAsync(PracticeAttendanceId id)
    {
        var deleteAttendance = await _dbContext.Practice_attendences.FirstOrDefaultAsync(x => x.Id == id);
        if(deleteAttendance is null)
        {
            _logger.LogError("Cannot find practice Attendance");
            return null;
        }
        _dbContext.Practice_attendences.Remove(deleteAttendance);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Deleted Practice Attendance");
        return deleteAttendance;
    }

    public async Task<IEnumerable<PracticeAttendance>> GetAllAsync(PracticeAttendanceQuery practiceAttendanceQuery)
    {
        _logger.LogInformation("Getting practice attendances.");

        var practiceAttendances = _dbContext.Practice_attendences.AsQueryable();

        if(practiceAttendanceQuery.PracticeId is not null && practiceAttendanceQuery.PracticeId != Guid.Empty)
        {
            var gameId = practiceAttendanceQuery.PracticeId.Value;
            practiceAttendances = practiceAttendances.Where(x => x.PracticeId == new PracticeId(gameId));
        }

        if(!string.IsNullOrWhiteSpace(practiceAttendanceQuery.LastName))
        {
            practiceAttendances = practiceAttendances.Where(x => x.Player.LastName.Contains(practiceAttendanceQuery.LastName));
        }

        var skipNumber = (practiceAttendanceQuery.PageNumber - 1) * practiceAttendanceQuery.PageSize;

        return await practiceAttendances
            .Skip(skipNumber)
            .Take(practiceAttendanceQuery.PageSize)
            .ToListAsync();
    }


    public async Task<PracticeAttendance?> GetByIdAsync(PracticeAttendanceId id)
    {
        var practiceAttendance = await _dbContext.Practice_attendences.FirstOrDefaultAsync(x => x.Id == id);

        if(practiceAttendance is null)
        {
            _logger.LogError("Cannot find Practice Attendance in the database");

            return null;
        }
        return practiceAttendance;
    }

    public async Task<IEnumerable<PracticeAttendance>> GetByPracticeAsync(PracticeId id)
    {
        _logger.LogInformation("Return all practice attendences from practice");
        return await _dbContext.Practice_attendences.Where(x => x.PracticeId == id).ToListAsync();
    }

    public async Task<PracticeAttendance?> RegisterAsync(PracticeAttendance practiceAttendance)

    {
        var newPracticeAttendance = await _dbContext.Practices.FirstOrDefaultAsync(x => x.Id == practiceAttendance.PracticeId);
        if (newPracticeAttendance is null)
        {
            _logger.LogWarning("Failed to register practice attendance in the database: {practice}", practiceAttendance);
            return null;
        }
        var entry = await _dbContext.Practice_attendences.AddAsync(practiceAttendance);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Registered practice in the database: {practice}", newPracticeAttendance);
        return entry.Entity;
    }
}