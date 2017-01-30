using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IGamAlmacena : IAlmacenaEntidad<Gam>, IGamConsulta
	{
        Task<ActualizarResponse<GamConMiembros>> ActualizarAsync(GamActualizar modelo, EntidadAutoinfo<Gam> autoinfoAlActualizar);

    }

	public interface IGamConsulta : IConsultaEntidad<Gam>
	{

		Task<QueryResponse<GamConMiembros>> ConsultarAsync(GamConMiembrosConsultar modelo,
		                                        Dictionary<string, string> peticion,
												Expression<Func<Gam, bool>> filtro = null);
	}
}

