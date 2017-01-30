using SeguimientoGam.Modelos.Peticiones;
using ServiceStack.FluentValidation;

namespace SeguimientoGam.Aut
{


    public class GamBorrarValidadorB : AbstractValidator<GamBorrar>
    {
        public GamReglasValidacion Reglas { get; set; }

        public GamBorrarValidadorB ()
        {
            RuleFor(x => x.Id)
                .NotEqual(0).WithMessage("Debe Indicar el GamId");

            RuleFor(x=>x.Id)
                .Must((e, v) => Reglas.ValidarEsPropietarioEncargadoAdminColaborardor(v,Request).Result)
                .WithName("Propietario")
                .WithMessage("Usuario no autorizado para Borrar el Gam con Id {0}", e=> e.Id);
        }
    }
}
