using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.TimeSheet
{
    public record CreateTimeSheetDto(double Hours, string Description, long TaskEntityId);
}
