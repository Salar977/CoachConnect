using CoachConnect.BusinessLayer.DTOs.Practice;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeMapper : IMapper<Practice, PracticeResponse>
{
    public PracticeResponse MapToDTO(Practice entity)
    {
        if(entity.Updated == DateTime.MinValue)
        {
            return new PracticeResponse(entity.Id.practiceId,
                                    entity.Location,
                                    entity.PracticeDate,
                                    entity.Created,
                                    null);
        }
        return new PracticeResponse(entity.Id.practiceId,
                                    entity.Location,
                                    entity.PracticeDate,
                                    entity.Created,
                                    entity.Updated);
    }

    public Practice MapToEntity(PracticeResponse practiceResponse)
    {
        throw new NotImplementedException();
    }
}