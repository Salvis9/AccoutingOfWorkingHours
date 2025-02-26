using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Task
{
    public record CreateTaskEntityDto(string Name, bool IsActive, long ReportId);
}
