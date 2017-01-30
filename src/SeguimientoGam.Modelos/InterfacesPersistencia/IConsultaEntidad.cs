using SeguimientoGam.Modelos.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{

	public interface IConsultaEntidad<TEntidad> where TEntidad : IEntidad
	{

		QueryResponse<TEntidad> Consultar(IQueryDb<TEntidad> modelo,
										  Dictionary<string, string> peticion,
										  Expression<Func<TEntidad, bool>> filtro = null);

		Task<QueryResponse<TEntidad>> ConsultarAsync(IQueryDb<TEntidad> modelo,
										  Dictionary<string, string> peticion,
										  Expression<Func<TEntidad, bool>> filtro = null);


		QueryResponse<Into> Consultar<Into>(IQueryDb<TEntidad, Into> modelo,
												  Dictionary<string, string> peticion,
												  Expression<Func<TEntidad, bool>> filtro = null);

		Task<QueryResponse<Into>> ConsultarAsync<Into>(IQueryDb<TEntidad, Into> modelo,
												  Dictionary<string, string> peticion,
												  Expression<Func<TEntidad, bool>> filtro = null);


		List<TEntidad> Consultar(Expression<Func<TEntidad, bool>> predicado);
		Task<List<TEntidad>> ConsultarAsync(Expression<Func<TEntidad, bool>> predicado);

		TEntidad ConsultarPorId(int id);
		Task<TEntidad> ConsultarPorIdAsync(int id);

		TEntidad ConsultarPorId(TEntidad modelo);
		Task<TEntidad> ConsultarPorIdAsync(TEntidad modelo);

        Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado);
        T ConsultarElPrimero<T>(Expression<Func<T, bool>> predicado);

    }

	public interface IConsultaEntidad : IDisposable
	{

		QueryResponse<From> Consultar<From>(IQueryDb<From> modelo,
											Dictionary<string, string> peticion,
											Expression<Func<From, bool>> filtro = null);


		Task<QueryResponse<From>> ConsultarAsync<From>(IQueryDb<From> modelo,
											Dictionary<string, string> peticion,
											Expression<Func<From, bool>> filtro = null);


		QueryResponse<Into> Consultar<From, Into>(IQueryDb<From, Into> modelo,
												  Dictionary<string, string> peticion,
												  Expression<Func<From, bool>> filtro = null);

		Task<QueryResponse<Into>> ConsultarAsync<From, Into>(IQueryDb<From, Into> modelo,
												  Dictionary<string, string> peticion,
												  Expression<Func<From, bool>> filtro = null);


		List<T> Consultar<T>(Expression<Func<T, bool>> predicado);
		Task<List<T>> ConsultarAsync<T>(Expression<Func<T, bool>> predicado);

		T ConsultarPorId<T>(int id);
		Task<T> ConsultarPorIdAsync<T>(int id);

		T ConsultarPorId<T>(T modelo) where T : IEntidad;
		Task<T> ConsultarPorIdAsync<T>(T modelo) where T : IEntidad;


        Task<T> ConsultarElPrimeroAsync<T>(Expression<Func<T, bool>> predicado);
        T ConsultarElPrimero<T>(Expression<Func<T, bool>> predicado);


    }


}
