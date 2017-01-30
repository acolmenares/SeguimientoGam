using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using System;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class MiembroGamCrear:Crear<MiembroGam>
    {
        public int GamId { get; set; }
            
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string TipoDocumento { get; set; }

        public string Documento { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Genero { get; set; }

    }
        
}
