using ExpeditionService.Api.ValidationRules;
using ExpeditionService.BusinessLogic.DTOs;
using FluentValidation;

namespace ExpeditionService.Api.Validators
{
    /// <summary>
    /// the validator for the package request
    /// </summary>
    public class PackageValidator : AbstractValidator<PackageDto>
    {
        /// <summary>
        /// Initialization of a new instance of <see cref="PackageValidator"/>
        /// </summary>
        public PackageValidator()
        {
            RuleFor(x => x.Weight)
                .WeightValidation();
            RuleFor(x => x.Dimensions)
                .DimensionsValidation();
            RuleFor(x => x.ContentDescription)
                .ContentDescriptionValidation();
        }
    }
}
