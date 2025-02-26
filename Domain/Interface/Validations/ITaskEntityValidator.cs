using Domain.Entity;
using Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Validations
{
    public interface ITaskEntityValidator : IBaseValidator<TaskEntity>
    {
        BaseResult CreateValidator(TaskEntity taskEntity, Report report);
    }
}
