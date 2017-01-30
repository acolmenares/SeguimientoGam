using ServiceStack;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Internal;
using ServiceStack.FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace SeguimientoGam.Aut
{
    public static class IValidatorExtensions
    {

        public static void ValidateAndThrow<T>(this IValidator<T> validator, T instance, IList<string> rules)
        {
            var validationResult = Validate(validator, instance, rules);
            if (!validationResult.IsValid)
            {
                throw validationResult.ToException();
            }
        }


        public static ValidationResult Validate<T>(this IValidator<T> validator, T instance, IList<string> rules)
        {
            var selector = new RulesetValidatorSelector(rules.ToArray());
            var context = new ValidationContext<T>(instance, new PropertyChain(), selector);
            return  validator.Validate(context);
            
        }


    }




}
