using Domain.Entity;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Validations
{
    public interface IReportValidator : IBaseValidator<Report>
    {
        // Проверяется наличие отчета, если отчет с переданным названием есть в бд, то содать точно такой же нельзя
        // Проверяется пользователь, если с UserId пользователь не найден, то такого пользователя нет
        BaseResult CreateValidator(Report report, User user);
    }
}
