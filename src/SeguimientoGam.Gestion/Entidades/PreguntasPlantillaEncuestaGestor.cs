using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Aut;

namespace SeguimientoGam.Gestion.Entidades
{
	public class PreguntasPlantillaEncuesta : Gestor<PreguntaPlantillaEncuesta>, IPreguntaPlantillaEncuestaGestor
	{

		public PreguntasPlantillaEncuesta(IPreguntaPlantillaEncuestaAlmacena repositorio) : base(repositorio)
		{
		}

		protected override void PreprocesarAlCrear(Crear<PreguntaPlantillaEncuesta> modelo,
		                                           EntidadAutoinfo<PreguntaPlantillaEncuesta> autoinfoAlCrear, 
		                                           IRequest peticion)
		{
			autoinfoAlCrear.SetValue(q => q.CreadoPorId, peticion.GetPersonaId());
			base.PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
		}
	}
}