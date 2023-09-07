using FluentValidation;
using ShipmentService.Api.ValidationRules;
using ShipmentService.BusinessLogic.DTOs;

namespace ShipmentService.Api.Validators
{
    /// <summary>
    /// the validator for the person request
    /// </summary>
    public class PersonValidator: AbstractValidator<PersonDto> 
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
