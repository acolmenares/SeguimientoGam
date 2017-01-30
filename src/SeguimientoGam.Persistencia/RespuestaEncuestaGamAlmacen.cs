using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using System;

namespace SeguimientoGam.Persistencia
{
	public class RespuestaEncuestaGamAlmacen : Almacen<RespuestaEncuestaGam>, IRespuestaEncuestaGamAlmacena
	{
		public RespuestaEncuestaGamAlmacen(IAlmacenaEntidad respositorio) : base(respositorio)
		{
		}

		protected override void PreprocesarAlCrear(RespuestaEncuestaGam entidad, 
		                                           EntidadAutoinfo<RespuestaEncuestaGam> autoinfoAlCrear)
		{
			autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
			base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
		}
	}
}
