using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using System;

namespace SeguimientoGam.Persistencia
{
	public class MiembroGamAlmacen : Almacen<MiembroGam>, IMiembroGamAlmacena
    {
        public MiembroGamAlmacen(IAlmacenaEntidad respositorio) : base(respositorio)
        {
        }

		protected override void PreprocesarAlCrear(MiembroGam entidad, EntidadAutoinfo<MiembroGam> autoinfoAlCrear)
        {

            autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
            base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
        }
    }
}
