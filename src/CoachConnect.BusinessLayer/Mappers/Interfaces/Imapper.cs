using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachConnect.BusinessLayer.Mappers.Interfaces;
public interface IMapper<TEntity, TDto>
{
    TDto MapToDTO(TEntity entity);
    TEntity MapToEntity(TDto dto);
}

