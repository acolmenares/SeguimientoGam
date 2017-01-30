using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.Peticiones
{
	public class EncuestaGamCrear : Crear<EncuestaGam>
	{
		public int GamId { get; set; }
		public int PlantillaEncuestaId { get; set; }
		public int? EventoCalendarioId { get; set; }
		public DateTime FechaRealizacion { get; set; }
		public short NumeroEncuentro { get; set; }
        public int? EncargadoId { get; set; }
    }

}
