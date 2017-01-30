using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.InterfacesPersistencia
{
	public interface IEncuestaGamAlmacena : IAlmacenaEntidad<EncuestaGam>, IEncuestaGamConsulta
	{
        Task<CrearResponse<EncuestaGamEventoCalendario>> CrearAsync(EncuestaGamCrear modelo, EntidadAutoinfo<EncuestaGam> autoinfo);
        Task<ActualizarResponse<EncuestaGamEventoCalendario>> ActualizarAsync(EncuestaGamActualizar modelo, EntidadAutoinfo<EncuestaGam> autoinfo);
    }

    public interface IEncuestaGamConsulta : IConsultaEntidad<EncuestaGam>
    {
        Task<QueryResponse<EncuestaGamRespuestas>> ConsultarAsync(EncuestaGamRespuestasConsultar modelo,
            Dictionary<string, string> peticion,
            Expression<Func<EncuestaGam, bool>> filtro = null);

        Task<QueryResponse<EncuestaGamEventoCalendario>> ConsultarAsync(EncuestaGamConsultar modelo, Dictionary<string, string> peticion, Expression<Func<EncuestaGam, bool>> filtro = null);

        

    }
}