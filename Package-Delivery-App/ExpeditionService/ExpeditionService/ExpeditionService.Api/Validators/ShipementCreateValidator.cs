using ExpeditionService.Api.ValidationRules;
using ExpeditionService.BusinessLogic.DTOs;
using FluentValidation;

namespace ExpeditionService.Api.Validators
{
    /// <summary>
    /// the validator for the shipementcreate request
    /// </summary>
    public class ShipmentCreateValidator : AbstractValidator<ShipmentDto> 
    {
        /// <summary>
        /// initialization of the shipementcreate request
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
