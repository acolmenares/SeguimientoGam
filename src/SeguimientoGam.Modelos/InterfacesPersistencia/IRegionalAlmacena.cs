using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{

	public interface IRegionalAlmacena : IRegionalConsulta, IAlmacenaEntidad<Regional>
    {

    }

    public interface IRegionalConsulta : IConsultaEntidad<Regional>
    {
        Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync(RegionalConMunicipiosConsultar modelo, 
		                                               Dictionary<string,string> peticion,
		                                               Expression<Func<Regional, bool>> filtro = null);

		Task<IList<int>> ConsultarRegionalIdsAsignadasAsync(int idPersona);

    }

}

