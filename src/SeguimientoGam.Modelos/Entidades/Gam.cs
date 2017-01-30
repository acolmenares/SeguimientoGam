using System;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack.DataAnnotations;

namespace SeguimientoGam.Modelos.Entidades
{
    public class Gam : IEntidad
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public int RegionalId { get; set; }
        public int MunicipioId { get; set; }
        [StringLength(256)]
        public string Nombre { get; set; }
        public int CreadoPorId { get; set; }
        public int EncargadoId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
