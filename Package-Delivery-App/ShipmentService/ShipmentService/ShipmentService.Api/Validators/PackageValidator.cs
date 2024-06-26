﻿using FluentValidation;
using ShipmentService.Api.Request;
using ShipmentService.Api.ValidationRules;

namespace ShipmentService.Api.Validators
{
    /// <summary>
    /// the validator for the package request
    /// </summary>
    public class PackageValidator:AbstractValidator<PackageRequest>
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
