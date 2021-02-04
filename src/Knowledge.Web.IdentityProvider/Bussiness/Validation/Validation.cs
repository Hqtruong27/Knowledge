using FluentValidation;
using Knowledge.Web.IdentityProvider.Models;

namespace Knowledge.Web.IdentityProvider.Bussiness.Validation
{
    public class RoleRequesetValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequesetValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id not empty")
                              .MaximumLength(50).WithMessage("Id A maximum of 50 characters");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name not empty");
        }
    }

}
