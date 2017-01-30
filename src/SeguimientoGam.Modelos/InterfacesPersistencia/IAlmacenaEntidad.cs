using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IAlmacenaEntidad<TEntidad> : IConsultaEntidad<TEntidad> where TEntidad : IEntidad
	{
		CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo);
		Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo);

		CrearResponse<TEntidad> Crear(Crear<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlActualizar);
		Task<CrearResponse<TEntidad>> CrearAsync(Crear<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlCrear);

		ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo);
		Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo);

		ActualizarResponse<TEntidad> Actualizar(Actualizar<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlActualizar);
		Task<ActualizarResponse<TEntidad>> ActualizarAsync(Actualizar<TEntidad> modelo, EntidadAutoinfo<TEntidad> autoinfoAlActualizar);

		BorrarResponse Borrar(int id);
		Task<BorrarResponse> BorrarAsync(int id);
	}

	public interface IAlmacenaEntidad : IConsultaEntidad, IDisposable
	{
		int Actualizar<T>(T modelo) where T : IEntidad;
		Task<int> ActualizarAsync<T>(T modelo) where T : IEntidad;

		int Actualizar<T>(T modelo, IList<string> updateonly) where T : IEntidad;
		Task<int> ActualizarAsync<T>(T modelo, IList<string> updateonly) where T : IEntidad;

		long Crear<T>(T modelo) where T : IEntidad;
		Task<long> CrearAsync<T>(T modelo) where T : IEntidad;

		int Borrar<T>(T modelo) where T : class, IEntidad;
		Task<int> BorrarAsync<T>(T modelo) where T : class, IEntidad;

		int Borrar<T>(int id) where T : IEntidad;
		Task<int> BorrarAsync<T>(int id) where T : IEntidad;
	}
}
