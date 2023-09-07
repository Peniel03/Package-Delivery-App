using FluentValidation;

namespace ExpeditionService.Api.ValidationRules
{
    /// <summary>
    /// the validation rules for the location's model
    /// </summary>
    public static class LocationValidationRules
    {
        /// <summary>
        /// Rule to validate the location name
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> LocationNameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Invalid LocationName");
            return builder;
        }

        /// <summary>
        /// Rule to validate the address
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> AddressValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Invalid Address");
            return builder;
        }

        /// <summary>
        /// Rule to validate the City
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> CityValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Invalid City");
            return builder;
        }

        /// <summary>
        /// Rule to validate the country.
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> CountryValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Invalid Country");
            return builder;
        }

        /// <summary>
        /// Rule to validate the postal code
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder option</param>
        /// <returns>A <seealso cref="IRuleBuilderOptions{T,string}"/></returns>
        public static IRuleBuilderOptions<T, string> PostalCodeValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(8)
                .WithMessage("Invalid PostalCode");
            return builder;
        }
    }
}
