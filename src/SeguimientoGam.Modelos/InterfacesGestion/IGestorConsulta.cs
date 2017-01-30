using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IGestorConsulta<TEntidad> where TEntidad : IEntidad
	{
		List<TEntidad> Consultar(Expression<Func<TEntidad, bool>> predicado);
		Task<List<TEntidad>> ConsultarAsync(Expression<Func<TEntidad, bool>> predicado);

		QueryResponse<TEntidad> Consultar(IQueryDb<TEntidad> modelo,
										  Dictionary<string, string> dynamicParams,
										  Expression<Func<TEntidad, bool>> filtro = null);

		Task<QueryResponse<TEntidad>> ConsultarAsync(IQueryDb<TEntidad> modelo,
										  Dictionary<string, string> dynamicParams,
										  Expression<Func<TEntidad, bool>> filtro = null);

		TEntidad ConsultarPorId(TEntidad modelo);
		Task<TEntidad> ConsultarPorIdAsync(TEntidad modelo);

		TEntidad ConsultarPorId(int id);
		Task<TEntidad> ConsultarPorIdAsync(int id);

        TEntidad ConsultarElPrimero(Expression<Func<TEntidad, bool>> predicado);
        Task<TEntidad> ConsultarElPrimeroAsync(Expression<Func<TEntidad, bool>> predicado);

        Func<TEntidad> GetEntityById(int id);
        //Task<Func<TEntidad>> GetEntityByIdAsync(int id);

    }
}