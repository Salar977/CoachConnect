using CoachConnect.DataAccess.Data;
using CoachConnect.DataAccess.Entities;
using CoachConnect.DataAccess.Repositories.Interfaces;
using CoachConnect.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoachConnect.DataAccess.Repositories;

public class PracticeRepository : IPracticeRepository
{
    private readonly CoachConnectDbContext _dbContext;
    private readonly ILogger<PracticeRepository> _logger;

    public PracticeRepository(CoachConnectDbContext dbContext,
                              ILogger<PracticeRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<IEnumerable<Practice>> GetAllAsync(PracticeQuery practiceQuery)
    {
        var practices = _dbContext.Practices.AsQueryable();
        
        var skipNumber = (practiceQuery.PageNumber - 1) * practiceQuery.PageSize;
        _logger.LogInformation("Get all practices - Repository");
        return await practices
            .OrderBy(p => p.Created)
            .Skip(skipNumber)
            .Take(practiceQuery.PageSize)
            .ToListAsync();
    }

    public async Task<Practice?> GetByIdAsync(PracticeId id)
    {
        var practice = await _dbContext.Practices.FirstOrDefaultAsync(x => x.Id == id);
        return practice ?? null;
    }

    public async Task<Practice?> RegisterPracticeAsync(Practice practice)
    {
        var newPractice = await _dbContext.Practices.AddAsync(practice);
        if (newPractice is null)
        {
            _logger.LogWarning("Failed to register practice in the database: {practice}", practice);
            return null;
        }
        
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Registered practice in the database: {practice}", practice);
        return newPractice.Entity;
    }

    public async Task<Practice?> UpdateAsync(PracticeId id, Practice practice)
    {
        var updatePractice = await _dbContext.Practices.FirstOrDefaultAsync(x => x.Id == id);
        if (updatePractice is null)
        {
            _logger.LogWarning("Failed to update practice in the database: {practice}", practice);
            return null;
        }
        
        updatePractice.Location = string.IsNullOrEmpty(practice.Location) ? updatePractice.Location : practice.Location;
        updatePractice.PracticeDate = string.IsNullOrEmpty(practice.PracticeDate.ToString()) ? updatePractice.PracticeDate : practice.PracticeDate;
        updatePractice.Updated = DateTime.Now;
        
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Updated practice in the database: {practice}", practice);
        return updatePractice;
    }

    public async Task<Practice?> GetByPracticeTimeAsync(DateTime dateTime)
    {
        _logger.LogDebug("Getting Game by time: {dateTime} from db", dateTime);

        // Get the start and end of the specified date
        DateTime startDate = dateTime.Date;
        DateTime endDate = startDate.AddDays(1);

        return await _dbContext.Practices
            .Where(d => d.PracticeDate >= startDate && d.PracticeDate < endDate)
            .FirstOrDefaultAsync();
    }



    public async Task<Practice?> DeleteAsync(PracticeId practiceId)
    {
        var deletePractice = await _dbContext.Practices.FirstOrDefaultAsync(x => x.Id == practiceId);
        if (deletePractice is null)
        {
            _logger.LogError("Failed to delete practice from the database: {practiceId}", practiceId);
            return null;
        }
        
        _dbContext.Practices.Remove(deletePractice);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Deleted practice from the database: {practiceId}", practiceId);
        return deletePractice;
    }
}