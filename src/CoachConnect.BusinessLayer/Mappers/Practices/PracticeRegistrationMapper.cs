using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeRegistrationMapper : IMapper<Practice, PracticeRequest>
{
    public PracticeRequest MapToDTO(Practice entity)
    {
        throw new NotImplementedException();
    }

    public Practice MapToEntity(PracticeRequest practiceRequest)
    {
        var now = DateTime.Now;
        return new Practice
        {
            Location = practiceRequest.Location,
            PracticeDate = practiceRequest.PracticeDate,
            Created = now,
            Updated = now
        };
    }
}