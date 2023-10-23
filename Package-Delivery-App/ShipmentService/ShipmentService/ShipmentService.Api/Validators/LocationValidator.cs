using FluentValidation;
using ShipmentService.Api.Request;
using ShipmentService.Api.ValidationRules;

namespace ShipmentService.Api.Validators
{
    /// <summary>
    /// The validator for the location request.
    /// </summary>
    public class LocationValidator:AbstractValidator<LocationRequest>
    {
        /// <summary>
        /// Initialization of a new instance of <see cref="LocationValidator"/>
        /// </summary>
        public LocationValidator()
        {
            RuleFor(x => x.LocationName)
                .LocationNameValidation();
            RuleFor(x => x.Address)
                .AddressValidation();
            RuleFor(x => x.City)
                .CityValidation();
            RuleFor(x => x.Country)
                .CountryValidation();
            RuleFor(x => x.PostalCode) 
                .PostalCodeValidation();
        }

    }
}
