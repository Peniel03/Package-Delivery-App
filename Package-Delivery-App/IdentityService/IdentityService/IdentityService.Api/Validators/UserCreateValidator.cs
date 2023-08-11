using FluentValidation;
using IdentityService.Api.Request;
using IdentityService.Api.ValidationRules;

namespace IdentityService.Api.Validators
{
    /// <summary>
    /// The validator for the user create request
    /// </summary>
    public class UserCreateValidator: AbstractValidator<UserCreateRequest>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserRequestCreateValidator"/>
        /// </summary>
        public UserCreateValidator()
        {
            RuleFor(x => x.Email)
               .EmailValidation();
            RuleFor(x => x.Password)
                .PasswordValidation();
            RuleFor(x => x.ConfirmedPassword)
                .Equal(x => x.Password);
            RuleFor(x => x.PhoneNumber)
                .PhoneNumberValidation();
            RuleFor(x => x.FirstName)
                .NameValidation();
            RuleFor(x => x.LastName)
                .NameValidation();
        }
    }
}
