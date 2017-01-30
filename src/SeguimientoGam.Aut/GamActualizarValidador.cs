using ServiceStack.FluentValidation;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Modelos.Extensiones;
using System.Threading.Tasks;

namespace SeguimientoGam.Aut
{
    public class GamActualizarValidador:AbstractValidator<GamActualizar>
	{
        public GamReglasValidacion Reglas { get; set; }

		public GamActualizarValidador()
		{
            
            RuleFor(x => x.Id)
                .NotEqual(0).WithMessage("Debe Indicar el GamId");

            //RuleFor(x => x.Nombre)
            //    .NotEmpty().WithMessage("Debe indicar el nombre del GAM");
            
            RuleFor(x => x.Id)
                .Must(  (e, gamiId) =>  ValidarEsPropietarioAdminColaborardor(gamiId).Result )
                .WithName("Propietario")
                .WithMessage("Usuario no autorizado para realizar cambios en el Gam con Id:{0}", e=>e.Id);

            When(x => x.EncargadoId.NoEsNullNiCero(), () => 
            {
                RuleFor(x=>x.Id).Must( (e,gamId)=> ValidarCambioDelEncargado(gamId, e.EncargadoId).Result )
                .WithName("Propietario")
                .WithMessage("Usuario no autorizado para cambiar el Encargado en el Gam con Id:{0}", e=>e.Id);
            });

        }

        private async Task<bool> ValidarCambioDelEncargado(int gamId, int? encargadoId)
        {
            return  await Reglas.ValidarCambioDelEncargado(gamId, encargadoId, Request);
        }

        private async Task<bool> ValidarEsPropietarioAdminColaborardor( int gamId)
        {            
            return await Reglas.ValidarEsPropietarioEncargadoAdminColaborardor(gamId, Request);
        }


    }
}

