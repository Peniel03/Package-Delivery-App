using FluentValidation;
using System.Runtime.CompilerServices;

namespace ShipmentService.Api.ValidationRules
{
    /// <summary>
    /// validation rules for the person's model
    /// </summary>
    public static class PersonValidationRules
    {
        /// <summary>
        /// Rule to validate the name
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">The rule builder option</param>
        /// <returns>A <see cref="IRuleBuilderOptions{T, string}"/></returns>
        public static IRuleBuilderOptions<T,string> NameValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30)
                .WithMessage("Invalid Name");

            return builder;
        }

        /// <summary>
        /// Rule to validate the phone number
        /// </summary>
        /// <typeparam name="T">T is <see cref="string"/></typeparam>
        /// <param name="ruleBuilder">the rule builder</param>
        /// <returns>A <see cref="IRuleBuilderOptions{T, string}"/></returns>
        public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builder = ruleBuilder
                .NotEmpty()
                .MinimumLength(9)
                .Matches(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|
                    2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|
                    4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$")
                .WithMessage("Invalid phone Number");

            return builder;
        }



    }
}
