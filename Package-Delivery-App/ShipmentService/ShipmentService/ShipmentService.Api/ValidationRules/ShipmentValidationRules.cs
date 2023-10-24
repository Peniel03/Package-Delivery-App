using FluentValidation;

namespace ShipmentService.Api.ValidationRules
{
    /// <summary>
    /// the validation rules for the shipment's model
    /// </summary>
    public static class ShipmentValidationRules
    {
        // <summary>
        /// Rule to validate the delivery method
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">The rule builder option</param>
        /// <returns>A <see cref="IRuleBuilderOptions{T, string}"/></returns>
        public static IRuleBuilderOptions<T, string> DeliveryMethodValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        { 
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(15)
                .MaximumLength(15)
                .WithMessage("Invalid delivery method");
            return builder;
        }

        // <summary>
        /// Rule to validate the shipemnt status
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">The rule builder option</param>
        /// <returns>A <see cref="IRuleBuilderOptions{T, string}"/></returns>
        public static IRuleBuilderOptions<T, string> ShipmentStatusValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder 
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15)
                .WithMessage("Invalid shipment status");
            return builder;
        }

        /// <summary>
        /// Rule to validate the shipment cost
        /// </summary>
        /// <typeparam name="T">T is <see cref="decimal"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,decimal}"/></returns>
        public static IRuleBuilderOptions<T, decimal> ShipmentCostValidation<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            var builder = ruleBuilder  
                .NotEmpty()
                .ScalePrecision(6,2)
                .GreaterThan(0)
                .WithMessage("Invalid shipment cost");
            return builder; 
        }
    }
}
