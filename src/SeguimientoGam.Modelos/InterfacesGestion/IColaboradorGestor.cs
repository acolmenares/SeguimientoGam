using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Autenticacion;
using ServiceStack;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	
	public interface IColaboradorGestor : IColaboradorGestorConsultas, IGestor<Colaborador>
	{
	}

	public interface IColaboradorGestorConsultas : IGestorConsulta<Colaborador>
	{
		List<Colaborador> Consultar();
		Task<List<Colaborador>> ConsultarAsync();
	}
}
