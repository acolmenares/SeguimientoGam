using ServiceStack.FluentValidation;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Modelos.Extensiones;
using System.Threading.Tasks;

namespace SeguimientoGam.Aut
{
    public class RespuestaEncuestaCrearValidador : AbstractValidator<RespuestaEncuestaGamCrear>
    {
        public RespuestaEncuestaGamReglasValidacion Reglas { get; set; }

        public RespuestaEncuestaCrearValidador()
        {

            RuleFor(x => x.MiembroGamId)
                .Must((e,id)=> EsRespuestaUnica(e).Result).WithMessage("El participante ya registro su respuesta");

        }

        private async Task<bool> EsRespuestaUnica(RespuestaEncuestaGamCrear modelo)
        {
            return await Reglas.EsRespuestaUnicaPorParticipante(modelo, Request);
        }

     
    }
}
