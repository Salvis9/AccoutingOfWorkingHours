using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Task
{
    public record UpdateTaskEntityDto(long Id, string Name, bool IsActive);
}
