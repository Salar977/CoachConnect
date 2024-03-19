using CoachConnect.BusinessLayer.DTOs.Practices;
using CoachConnect.BusinessLayer.Mappers.Interfaces;
using CoachConnect.DataAccess.Entities;

namespace CoachConnect.BusinessLayer.Mappers.Practices;

public class PracticeUpdateMapper : IMapper<Practice, PracticeUpdate>
{
    public PracticeUpdate MapToDTO(Practice entity)
    {
        throw new NotImplementedException();
    }

    public Practice MapToEntity(PracticeUpdate practiceUpdate)
    {
        return new Practice
        {
            Location = practiceUpdate.Location!,
            PracticeDate = (DateTime) practiceUpdate.PracticeDate!,
            Updated = DateTime.Now
        };
    }
}