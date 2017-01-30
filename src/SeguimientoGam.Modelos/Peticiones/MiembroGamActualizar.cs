using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.Peticiones
{
	public class MiembroGamActualizar:Actualizar<MiembroGam>
    {
                               
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string TipoDocumento { get; set; }

        public string Documento { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Genero { get; set; }
    }

    
}
