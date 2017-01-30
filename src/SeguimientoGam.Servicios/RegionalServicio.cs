using ServiceStack;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;

namespace SeguimientoGam.Servicios
{
    [Authenticate]
    public class RegionalServicio:Service
    {
        
        public IRegionalGestor Regionales { get; set; }
   
        public QueryResponse<RegionalConMunicipios> Get(RegionalConMunicipiosConsultar modelo)
        {		
			return Regionales.ConsultarAsync(modelo, Request.GetRequestParams()).Result;
		}

		public QueryResponse<Regional> Get(RegionalConsultar modelo)
		{
			return Regionales.ConsultarAsync(modelo,Request.GetRequestParams()).Result;
		}

        public CrearResponse<Regional> Post(RegionalCrear modelo)
        {
			return Regionales.CrearAsync(modelo,Request).Result; 
        }
    }
}
