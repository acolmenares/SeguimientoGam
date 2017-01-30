using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Aut;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Modelos.Extensiones;


namespace SeguimientoGam.Gestion.Entidades
{
	public class RespuestasEncuestaGam : Gestor<RespuestaEncuestaGam>, IRespuestaEncuestaGamGestor
	{

		public RespuestasEncuestaGam(IRespuestaEncuestaGamAlmacena repositorio) : base(repositorio)
		{
		}
        
		protected override void PreprocesarAlCrear(Crear<RespuestaEncuestaGam> modelo,
		                                           EntidadAutoinfo<RespuestaEncuestaGam> autoinfoAlCrear, IRequest peticion)
		{

            RespuestaEncuestaGamCrear modeloCrear = modelo as RespuestaEncuestaGamCrear;
            
            modeloCrear.PreguntaPlantillaEncuestaId.SiEsCeroEntonces(() => 
            {
                var preguntaPlantillaPorDefecto = GetPreguntaPlantillaPorDefecto(peticion,modeloCrear);
                modeloCrear.PreguntaPlantillaEncuestaId = preguntaPlantillaPorDefecto.Id;
            });


            autoinfoAlCrear.SetValue(q => q.CreadoPorId, peticion.GetPersonaId());
			base.PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
		}

        private PreguntaPlantillaEncuesta GetPreguntaPlantillaPorDefecto(IRequest peticion, RespuestaEncuestaGamCrear modeloCrear)
        {
            var encuestaGam = peticion.TryResolve<IEncuestaGamGestorConsultas>().ConsultarPorId(modeloCrear.EncuestaGamId);
            var plantillaEncuestaId = encuestaGam.PlantillaEncuestaId;

            var preguntaPlantillasEncuestasGestor = peticion.TryResolve<IPreguntaPlantillaEncuestaGestorConsultas>();

            var codigoPreguntaComoMeSiento = GetValores(peticion).AlivioEmocionalParametros.CodigoEPreguntaComoMeSiento;

            return preguntaPlantillasEncuestasGestor
                .ConsultarElPrimero(q =>q.PlantillaEncuestaId==  plantillaEncuestaId &&  q.Codigo == codigoPreguntaComoMeSiento);
            
        }

    }
}