using ServiceStack.FluentValidation;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.FluentValidation.Results;

namespace SeguimientoGam.Aut
{
    public class CustomValidationException : ValidationException,  IResponseStatusConvertible
    {
        public CustomValidationException(string propertyName, string error):base( new ValidationFailure[] {
            new ValidationFailure(propertyName, error, "")
        }) 
        {
        }

        
        //IEnumerable<ValidationFailure> errors

    }
}
