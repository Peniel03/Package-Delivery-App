using FluentValidation;
using IdentityService.Api.Request;
using IdentityService.Api.ValidationRules;

namespace IdentityService.Api.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class UserUpdateValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Email)
                   .EmailValidation();
            RuleFor(x => x.Password)
                    .PasswordValidation();
            RuleFor(x => x.PhoneNumber)
                    .PhoneNumberValidation();
            RuleFor(x => x.FirstName)
                    .NameValidation();
            RuleFor(x => x.LastName)
                    .NameValidation();
        }
    }
}
