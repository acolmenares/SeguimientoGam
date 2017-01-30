using System.Collections.Generic;
using ServiceStack;
using SeguimientoGam.Modelos.Entidades;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class RegionalConMunicipiosConsultar: QueryDb<Regional, RegionalConMunicipios>
    {
    }

   
    public class RegionalConMunicipios : Regional
    {
        public RegionalConMunicipios()
        {
            Municipios = new List<Municipio>();
        }
                
        public List<Municipio> Municipios { get; set; }
    } 
}
