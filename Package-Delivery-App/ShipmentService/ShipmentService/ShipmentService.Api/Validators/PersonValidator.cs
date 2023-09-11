using FluentValidation;
using ShipmentService.Api.Request;
using ShipmentService.Api.ValidationRules;

namespace ShipmentService.Api.Validators
{
    /// <summary>
    /// the validator for the person request
    /// </summary>
    public class PersonValidator: AbstractValidator<PersonRequest>
    {
        /// <summary>
        /// initialization of the person request 
        /// </summary>
        public PersonValidator()
        {
            RuleFor(x => x.Name)
                .NameValidation();
            RuleFor(x => x.Phone)
                .PhoneNumberValidation();   
        }
    }
}
