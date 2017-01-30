using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;

namespace SeguimientoGam.Modelos.Peticiones
{
	public class RegionalCrear:Crear<Regional>
    {
        
        public string Nombre { get; set; }
                
        public string Codigo { get; set; }
                
        public int PaisId { get; set; }
                
        public int DepartamentoId { get; set; }
                
        public int MunicipioId { get; set; }
    }

    
}
