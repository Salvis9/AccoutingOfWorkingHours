using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.TimeSheet
{
    public record TimeSheetDto(long Id, double Hours, string Description, string CreatedAt);
}
