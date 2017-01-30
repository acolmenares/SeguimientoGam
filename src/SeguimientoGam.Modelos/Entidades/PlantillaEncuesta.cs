using System;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    public class PlantillaEncuesta:IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(8)]
        public string Codigo { get; set; }
        [StringLength(32)]
        public string Nombre { get; set; }
        [StringLength(128)]
        public string Descripcion { get; set; }
        public bool Aprobada { get; set; }
        public bool Inactiva { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int CreadoPorId { get; set; }

    }
}
