using Domain.Dto.TimeSheet;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.FluentValidations.TimeSheet
{
    public class UpdateTimeSheetValidator : AbstractValidator<UpdateTimeSheetDto>
    {
        public UpdateTimeSheetValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Hours).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        }
    }
}
