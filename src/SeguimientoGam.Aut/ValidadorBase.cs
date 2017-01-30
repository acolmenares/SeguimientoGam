using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Entidades;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Model;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.FluentValidation.Results;
using ServiceStack.FluentValidation.Internal;

namespace SeguimientoGam.Aut
{
    public abstract class ValidadorBase<T> : AbstractValidator<T> 
    {
        protected ValidadorBase()
        {
            RulesToAplyOnValidate = new List<string>();
        }

        protected IList<string> RulesToAplyOnValidate { get; set; }


        public override ValidationResult Validate(T instance)
        {
            ValidationContext<T> context = CreateContextForRulesToAplyOnValidate(instance);
            return Validate(context);
        }


        public override ValidationResult Validate(ValidationContext<T> context)
        {
            context = CreateContextForRulesToAplyOnValidate(context.InstanceToValidate, context.PropertyChain);
            return base.Validate(context);
        }


        private  ValidationContext<T> CreateContextForRulesToAplyOnValidate(T instance, PropertyChain propertychain=null)
        {
            var selector = new RulesetValidatorSelector(RulesToAplyOnValidate.ToArray());
            var context = new ValidationContext<T>(instance, new PropertyChain(), selector);
            return context;
        }



    }
}
