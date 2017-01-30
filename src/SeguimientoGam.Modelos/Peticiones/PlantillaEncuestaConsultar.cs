using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class PlantillaEncuestaConsultar : QueryDb<PlantillaEncuesta>
    {
    }

    public class PlantillaEncuestaCrear : Crear<PlantillaEncuesta>
    {

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Aprobada { get; set; }
        public bool? Inactiva { get; set; }
    }

    public class PlantillaEncuestaActualizar : Actualizar<PlantillaEncuesta>
    {
		public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Aprobada { get; set; }
        public bool? Inactiva { get; set; }
    }

	public class PlantillaEncuestaBorrar: Borrar<PlantillaEncuesta> {
        
    }

}
