using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;

namespace SeguimientoGam.Modelos.Entidades
{
    public class RespuestaEncuestaGam:IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int EncuestaGamId { get; set; } 
        public int MiembroGamId { get; set; }
        public int PreguntaPlantillaEncuestaId { get; set; }
        [StringLength(32)]
        public string Valor { get; set; }
        [StringLength(256)]
        public string Texto { get; set; }
        public DateTime FechaCreacion { get; set; }
		public int CreadoPorId { get; set; }
    }
}
