using Domain.Dto.Task;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.FluentValidations.Task
{
    public class CreateTaskEntityValidator : AbstractValidator<CreateTaskEntityDto>
    {
        public CreateTaskEntityValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        }
    }
}
