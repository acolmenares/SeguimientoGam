using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IRegionalGestor: IRegionalGestorConsultas, IGestor<Regional>
    {
    }

    public interface IRegionalGestorConsultas : IGestorConsulta<Regional>
    {
        QueryResponse<RegionalConMunicipios> Consultar(RegionalConMunicipiosConsultar modelo,
            Dictionary<string,string> peticion,
            Expression<Func<Regional, bool>> filtro = null);

		Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync(RegionalConMunicipiosConsultar modelo,
			Dictionary<string, string> peticion,
			Expression<Func<Regional, bool>> filtro = null);

		QueryResponse<RegionalConMunicipios> Consultar();

		Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync();

		Task<IList<int>> ConsultarRegionalIdsAsignadosAsync(int idPersona);

               
    }


}
