using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.TimeSummary
{
    public record TimeSummaryDto(double TotalHours, string Status);
}
