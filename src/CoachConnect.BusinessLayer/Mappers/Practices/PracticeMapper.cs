using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeMapper : IMapper<Practice, PracticeResponse>
{
    public PracticeResponse MapToDTO(Practice entity)
    {
        return new PracticeResponse(entity.Id.practiceId,
                                    entity.Location,
                                    entity.PracticeDate,
                                    entity.Created,
                                    entity.Updated,
                                    entity.PracticeAttendances);
    }

    public Practice MapToEntity(PracticeResponse practiceResponse)
    {
        throw new NotImplementedException();
    }
}