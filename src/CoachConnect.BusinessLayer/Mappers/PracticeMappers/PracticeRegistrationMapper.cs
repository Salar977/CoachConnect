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
        return new Practice
        {
            Location = practiceRequest.Location,
            PracticeDate = practiceRequest.PracticeDate,
            Created = DateTime.Now,
            Updated = DateTime.MinValue
        };
    }
}