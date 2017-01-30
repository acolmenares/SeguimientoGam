using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
namespace SeguimientoGam.Persistencia
{
	public class PreguntaPlantillaEncuestaAlmacen : Almacen<PreguntaPlantillaEncuesta>, 
	IPreguntaPlantillaEncuestaAlmacena
	{

		public PreguntaPlantillaEncuestaAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
		}


		protected override void PreprocesarAlCrear(PreguntaPlantillaEncuesta entidad,
		                                           EntidadAutoinfo<PreguntaPlantillaEncuesta> autoinfoAlCrear)
		{
			autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
			base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
		}


	}
}

