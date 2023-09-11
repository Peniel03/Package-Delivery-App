using FluentValidation;
using ShipmentService.Api.Request;
using ShipmentService.Api.ValidationRules;

namespace ShipmentService.Api.Validators
{
    public class ShipmentCreateValidator: AbstractValidator<ShipmentCreateRequest>
    {
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
