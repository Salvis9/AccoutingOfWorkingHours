using Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface.Validations;
using Domain.Result;
using Domain.Entity;
using Application.Resources;
using Domain.Enum;

namespace Application.Validations
{
    public class ReportValidator : IReportValidator
    {
        public BaseResult ValidateOnNull(Report model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                };
            }
            return new BaseResult();
        }

        public BaseResult CreateValidator(Report report, User user)
        {
            // Валидатор необходим для гибкости метода создания отчета (можно писать проверки на null в CreateReportAsync,но лучше добавить гибкости)
            if (report != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReportAlreadyExists,
                    ErrorCode = (int)ErrorCodes.ReportAlreadyExists
                };
            }

            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
            return new BaseResult();
        }
    }
}
