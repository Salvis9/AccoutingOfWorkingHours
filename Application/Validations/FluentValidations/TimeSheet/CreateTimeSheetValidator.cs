using Domain.Dto.TimeSheet;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.FluentValidations.TimeSheet
{
    public class CreateTimeSheetValidator : AbstractValidator<CreateTimeSheetDto>
    {
        public CreateTimeSheetValidator()
        {
            RuleFor(x => x.Hours).NotEmpty().InclusiveBetween(0,24);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
