using Domain.Entity;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Validations
{
    public interface ITimeSheetValidator : IBaseValidator<TimeSheet>
    {
        BaseResult CreateValidator(TimeSheet timeSheet, TaskEntity taskEntity);
        BaseResult HoursLimitPerDayValidator(TimeSheet timeSheet, double existingHours);
    }
}
