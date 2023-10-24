using ExpeditionService.Api.ValidationRules;
using ExpeditionService.BusinessLogic.DTOs;
using FluentValidation;

namespace ExpeditionService.Api.Validators
{
        /// <summary>
        /// the validator for the person request
        /// </summary> 
        public class PersonValidator : AbstractValidator<PersonDto> 
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
