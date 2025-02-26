using Domain.Dto.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.FluentValidations.Task
{
    public class UpdateTaskEntityValidator : AbstractValidator<UpdateTaskEntityDto>
    {
        public UpdateTaskEntityValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.IsActive).NotEmpty();
        }
    }
}
