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
    public class TaskEntityValidator : ITaskEntityValidator
    {
        public BaseResult ValidateOnNull(TaskEntity model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TasksEntityNotFound,
                    ErrorCode = (int)ErrorCodes.TasksEntityNotFound
                };
            }
            return new BaseResult();
        }

        public BaseResult CreateValidator(TaskEntity taskEntity, Report report)
        {
            if (taskEntity != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.TaskEntityAlreadyExists,
                    ErrorCode = (int)ErrorCodes.TaskEntityAlreadyExists
                };
            }

            if (report == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                };
            }
            if (!report.IsActive)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportInactive,
                    ErrorCode = (int)ErrorCodes.ReportInactive
                };
            }
            return new BaseResult();
        }
    }
}
