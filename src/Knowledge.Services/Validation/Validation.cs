using FluentValidation;
using Knowledge.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Services.Validation
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id not empty")
                              .MaximumLength(50).WithMessage("Id A maximum of 50 characters");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name not empty");
        }
    }

}
