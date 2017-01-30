
using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Extensiones;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Peticiones;

namespace SeguimientoGam.Aut
{
    public class RespuestaEncuestaGamReglasValidacion
    {
        
        public async Task<bool> EsRespuestaUnicaPorParticipante(RespuestaEncuestaGamCrear modelo, IRequest peticion)
        {
            var respuestaParticipante = await ConsultarRespuestaDelParticipante(modelo, peticion);

            return respuestaParticipante.Id == 0;
        }

        
        private async Task<RespuestaEncuestaGam> ConsultarRespuestaDelParticipante(RespuestaEncuestaGamCrear modelo , IRequest peticion)
        {
            var gestor = peticion.TryResolve<IRespuestaEncuestaGamGestorConsultas>();

            var encuestaGamId = modelo.EncuestaGamId;
            var miembroId = modelo.MiembroGamId;
            var preguntaId = modelo.PreguntaPlantillaEncuestaId;

            return await gestor.ConsultarElPrimeroAsync(q => q.EncuestaGamId == encuestaGamId && q.MiembroGamId == miembroId && q.PreguntaPlantillaEncuestaId == preguntaId);
        }


    }
}
