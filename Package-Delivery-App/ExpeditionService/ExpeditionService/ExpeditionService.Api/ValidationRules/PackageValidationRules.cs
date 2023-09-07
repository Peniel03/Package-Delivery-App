using FluentValidation;

namespace ExpeditionService.Api.ValidationRules
{
    /// <summary>
    /// the validation rules for the package's model
    /// </summary>
    public static class PackageValidationRules
    {
        /// <summary>
        /// Rule to validate the package dimensions
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> DimensionsValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Invalid Dimensions");
            return builder;
        }

        /// <summary>
        /// Rule to validate the package weight
        /// </summary>
        /// <typeparam name="T">T is <see cref="decimal"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,decimal}"/></returns>
        public static IRuleBuilderOptions<T, decimal> WeightValidation<T>(this IRuleBuilder<T, decimal> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotEmpty()
                .ScalePrecision(4, 10)
                .GreaterThan(0)
                .WithMessage("Invalid weight");
            return builder;
        }

        /// <summary>
        /// Rule to validate the package content description
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> ContentDescriptionValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(40)
                .WithMessage("Invalid content description");
            return builder;
        }
    }
}
