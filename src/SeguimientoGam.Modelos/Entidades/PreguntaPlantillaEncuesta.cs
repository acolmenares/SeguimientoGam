using System;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    public class PreguntaPlantillaEncuesta : IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(8)]
        public string Codigo { get; set; }
        [StringLength(128)]
        public string Texto { get; set; }
        [StringLength(32)]
        public int PlantillaEncuestaId { get; set; }
        public string TipoRespuesta { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int CreadoPorId { get; set; }
    }
}