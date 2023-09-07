﻿using ExpeditionService.Api.ValidationRules;
using ExpeditionService.BusinessLogic.DTOs;
using FluentValidation;

namespace ExpeditionService.Api.Validators
{
    public class ShipmentUpdateValidator
    {
        /// <summary>
        /// the validator for the shipment update request
        /// </summary>
        public class ShipmentUpdateValiator : AbstractValidator<ShipmentDto>
        {
            /// <summary>
            /// initialisation of a new instance of <see cref="ShipmentUpdateValiator"/>
            /// </summary>
            public ShipmentUpdateValiator()
            {
                RuleFor(x => x.ShipmentCost)
                    .ShipmentCostValidation();
                RuleFor(x => x.DeliveryMethod)
                    .DeliveryMethodValidation();
                RuleFor(x => x.ActualDeliveryDateTime)
                   .NotEmpty()
                   .NotNull();
                RuleFor(x => x.ShipmentStatus)
                    .ShipmentStatusValidation();
            }
        }
    }
}
