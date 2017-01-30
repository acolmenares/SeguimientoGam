using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;
using System;

namespace SeguimientoGam.Modelos.Entidades
{
    public class MiembroGam : IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }

        public int GamId { get; set; }

        [StringLength(64)]
        public string Nombres { get; set; }

        [StringLength(64)]
        public string Apellidos { get; set; }

        [StringLength(16)]
        public string TipoDocumento { get; set; }

        [StringLength(16)]
        public string Documento { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(16)]
        public string Genero { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int CreadoPorId { get; set; }
        

    }
}
