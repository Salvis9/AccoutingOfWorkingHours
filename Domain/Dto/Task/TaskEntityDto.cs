using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Task
{
    public record TaskEntityDto(long Id, string Name, bool IsActive, string CreatedAt);
}
