using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Autenticacion;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;

namespace SeguimientoGam.Persistencia
{
	public class RegionalAlmacen : Almacen<Regional>, IRegionalAlmacena
	{
		readonly IAlmacenaEntidad repositorio;
		public RegionalAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
			this.repositorio = repositorio;
		}

		public async Task<QueryResponse<RegionalConMunicipios>> ConsultarAsync(RegionalConMunicipiosConsultar modelo,
															   Dictionary<string, string> peticion,
															   Expression<Func<Regional, bool>> filtro = null)
		{
			var r = await repositorio.ConsultarAsync(modelo, peticion, filtro);
			var municipios = await repositorio.ConsultarAsync<Municipio>(q => r.Results.Select(x => x.DepartamentoId).Contains(q.DepartamentoId));

			r.Results.ForEach(x =>
			{
				x.Municipios = municipios.FindAll(y => y.DepartamentoId == x.DepartamentoId).ToList();
			});

			return r;
		}

		public async Task<IList<int>> ConsultarRegionalIdsAsignadasAsync(int idPersona)
		{

			var lista = new List<int>();
			var persona = await repositorio.ConsultarPorIdAsync<Persona>(idPersona);

			if (persona.Id != 0)
			{
				lista.Add(persona.RegionalId);
				var personaRegionales = await repositorio.ConsultarAsync<PersonaRegional>(q => q.PersonaId == idPersona);
				personaRegionales.ForEach(r =>
				{
					lista.AddIfNotExists(r.RegionalId);
				});

			};

			return lista;

		}

	}
}

