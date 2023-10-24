using FluentValidation;
using ShipmentService.Api.ValidationRules;
using ShipmentService.BusinessLogic.DTOs;

namespace ShipmentService.Api.Validators
{
    /// <summary>
    /// the validator for the shipmentcreate request
    /// </summary>
    public class ShipmentCreateValidator: AbstractValidator<ShipmentDto>
    {
        /// <summary>
        /// initialization of the shipmentcreate request
        /// </summary>
        public ShipmentCreateValidator()
        {
            RuleFor(x => x.PickupDateTime)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.ShipmentCost)
                .ShipmentCostValidation();
            RuleFor(x => x.DeliveryMethod)
                .DeliveryMethodValidation();
            RuleFor(x => x.EstimatedDeliveryDateTime)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ActualDeliveryDateTime)
               .NotEmpty()
               .NotNull();
            RuleFor(x => x.ShipmentStatus)
                .ShipmentStatusValidation();
        }
    }
}
