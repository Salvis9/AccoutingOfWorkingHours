using Application.Resources;
using Domain.Entity;
using Domain.Enum;
using Domain.Interface.Validations;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class TimeSheetValidator : ITimeSheetValidator
    {
        public BaseResult ValidateOnNull(TimeSheet model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TimeSheetsNotFound,
                    ErrorCode = (int)ErrorCodes.TimeSheetsNotFound
                };
            }
            return new BaseResult();
        }

        public BaseResult CreateValidator(TimeSheet timeSheet, TaskEntity taskEntity)
        {
            if (timeSheet != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TimeSheetAlreadyExists,
                    ErrorCode = (int)ErrorCodes.TimeSheetAlreadyExists
                };
            }

            if (taskEntity == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TaskEntityNotFound,
                    ErrorCode = (int)ErrorCodes.TaskEntityNotFound
                };
            }

            if (!taskEntity.IsActive)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TaskInactive,
                    ErrorCode = (int)ErrorCodes.TaskInactive
                };
            }
            return new BaseResult();
        }

        public BaseResult HoursLimitPerDayValidator(TimeSheet timeSheet, double existingHours)
        {
            if (existingHours + timeSheet.Hours > 24)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TaskInactive,
                    ErrorCode = (int)ErrorCodes.TaskInactive
                };
            }
            return new BaseResult();
        }
    }
}
