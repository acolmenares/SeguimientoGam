using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Interfaces;
namespace SeguimientoGam.Persistencia
{
	public class PlantillaEncuestaAlmacen : Almacen<PlantillaEncuesta>, IPlantillaEncuestaAlmacena
	{

		public PlantillaEncuestaAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
		}


		protected override void PreprocesarAlCrear(PlantillaEncuesta entidad, EntidadAutoinfo<PlantillaEncuesta> autoinfoAlCrear)
		{
			autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
			base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
		}


	}
}

