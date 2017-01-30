using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Gestion.Entidades
{
	public class Regionales : Gestor<Regional>,  IRegionalGestor
    {
		readonly IRegionalAlmacena repositorio;

		public Regionales(IRegionalAlmacena repositorio):base(repositorio)
        {
            this.repositorio = repositorio;
        }
                

		public async Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync(RegionalConMunicipiosConsultar modelo, 
            Dictionary<string,string> peticion,
            Expression<Func<Regional, bool>> filtro = null)
        {

			return await repositorio.ConsultarAsync(modelo, peticion);
        }



		public QueryResponse<RegionalConMunicipios> Consultar(RegionalConMunicipiosConsultar modelo, Dictionary<string, string> peticion, Expression<Func<Regional, bool>> filtro)
		{
			return ConsultarAsync(modelo, peticion, filtro).Result;
		}

		public QueryResponse<RegionalConMunicipios> Consultar()
		{
			return ConsultarAsync(new RegionalConMunicipiosConsultar(),
			                      new Dictionary<string, string>()).Result;
		}

		public Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync()
		{
			return ConsultarAsync(new RegionalConMunicipiosConsultar(),
								  new Dictionary<string, string>());
		}

		public async Task<IList<int>> ConsultarRegionalIdsAsignadosAsync(int idPersona)
		{
			return await repositorio.ConsultarRegionalIdsAsignadasAsync(idPersona);
		}
	}
}
