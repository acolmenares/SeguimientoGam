using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.InterfacesGestion
{
	public interface IEncuestaGamGestor : IEncuestaGamGestorConsultas, IGestor<EncuestaGam>
	{
        Task<CrearResponse<EncuestaGamEventoCalendario>> CrearAsync(EncuestaGamCrear modelo, IRequest peticion);
        Task<ActualizarResponse<EncuestaGamEventoCalendario>> ActualizarAsync(EncuestaGamActualizar modelo, IRequest peticion);
    }

    public interface IEncuestaGamGestorConsultas : IGestorConsulta<EncuestaGam>
    {
        Task<QueryResponse<EncuestaGamRespuestas>> ConsultarAsync(EncuestaGamRespuestasConsultar modelo,
            Dictionary<string, string> peticion,
            Expression<Func<EncuestaGam, bool>> filtro = null);

        Task<QueryResponse<EncuestaGamEventoCalendario>> ConsultarAsync(EncuestaGamConsultar modelo, IRequest peticion);

    }
}