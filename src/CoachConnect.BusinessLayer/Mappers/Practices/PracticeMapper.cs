using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeMapper : IMapper<Practice, PracticeResponse>
{
    public PracticeResponse MapToDTO(Practice entity)
    {
        return new PracticeResponse(entity.Id,
                                    entity.Location,
                                    entity.PracticeDate,
                                    entity.Created,
                                    entity.Updated,
                                    entity.PracticeAttendances);
    }

    public Practice MapToEntity(PracticeResponse practiceResponse)
    {
        var now = DateTime.Now;
        return new Practice
        {
            Id = practiceResponse.PracticeId,
            Location = practiceResponse.Location,
            PracticeDate = practiceResponse.PracticeDate,
            Created = now,
            Updated = now,
            PracticeAttendances = practiceResponse.PracticeAttendances!
        };
    }
}