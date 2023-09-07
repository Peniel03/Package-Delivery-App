using FluentValidation;
using IdentityService.Api.Request;
using IdentityService.Api.ValidationRules;

namespace IdentityService.Api.Validators
{
    /// <summary>
    /// The validator fot he user login request
    /// </summary>
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailValidation();
            RuleFor(x => x.Password)
                .PasswordValidation();
        }
    }
}
