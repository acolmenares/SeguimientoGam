using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using System;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using SeguimientoGam.Modelos.Extensiones;

namespace SeguimientoGam.Persistencia
{
	public class EncuestaGamAlmacen : Almacen<EncuestaGam>, IEncuestaGamAlmacena
	{
        readonly IAlmacenaEntidad repositorio;

        public EncuestaGamAlmacen(IAlmacenaEntidad repositorio) : base(repositorio)
		{
            this.repositorio = repositorio;
		}


        public async Task<CrearResponse<EncuestaGamEventoCalendario>> CrearAsync(EncuestaGamCrear modelo, EntidadAutoinfo<EncuestaGam> autoinfoAlCrear)
        {
            var encuestagam = await base.CrearAsync(modelo, autoinfoAlCrear);
            var conEvento = new EncuestaGamEventoCalendario();

            conEvento.PopulateWith(encuestagam.Data);

            if (conEvento.EventoCalendarioId.HasValue && conEvento.EventoCalendarioId.Value != 0)
            {
                conEvento.EventoCalendario = await repositorio.ConsultarPorIdAsync<EventoCalendario>(conEvento.EventoCalendarioId.Value);
            }

            return new CrearResponse<EncuestaGamEventoCalendario> { Data = conEvento };
        }

        public async Task<ActualizarResponse<EncuestaGamEventoCalendario>> ActualizarAsync(EncuestaGamActualizar modelo, EntidadAutoinfo<EncuestaGam> autoinfoAlActualizar)
        {
            var encuestagam = await base.ActualizarAsync(modelo,autoinfoAlActualizar);
            var conEvento = new EncuestaGamEventoCalendario();

            conEvento.PopulateWith(encuestagam.Data);

            if (conEvento.EventoCalendarioId.HasValue && conEvento.EventoCalendarioId.Value != 0)
            {
                conEvento.EventoCalendario= await  repositorio.ConsultarPorIdAsync<EventoCalendario>(conEvento.EventoCalendarioId.Value);
            }
            
            return new ActualizarResponse<EncuestaGamEventoCalendario> { Data = conEvento };
        }

        public async Task<QueryResponse<EncuestaGamEventoCalendario>> ConsultarAsync(EncuestaGamConsultar modelo, Dictionary<string, string> peticion, Expression<Func<EncuestaGam, bool>> filtro = null)
        {
            var encuestas = await  base.ConsultarAsync(modelo, peticion, filtro);

            var eventoIds = encuestas.Results
                .Where(x=>x.EventoCalendarioId.HasValue && x.EventoCalendarioId.Value!=0)
                .ToList()
                .ConvertAll(x => x.EventoCalendarioId);
            var eventos = await repositorio.ConsultarAsync<EventoCalendario>(q => eventoIds.Contains(q.Id));

            eventos.ForEach(ev => {
                encuestas.Results.FindAll(enc => enc.EventoCalendarioId == ev.Id).ForEach(f =>
                {
                    f.EventoCalendario = ev;
                });
            });
           
            return encuestas;
        }


        public async Task<QueryResponse<EncuestaGamRespuestas>> ConsultarAsync(EncuestaGamRespuestasConsultar modelo, Dictionary<string, string> peticion, Expression<Func<EncuestaGam, bool>> filtro = null)
        {
            var encuestas= await repositorio.ConsultarAsync(modelo, peticion, filtro);
                        
            var gamIdsEnEncuestas = encuestas.Results.ConvertAll(x => x.GamId);  // los gams de las encuestas...
            var miembrosGamsEnEncuestas = await repositorio.ConsultarAsync<MiembroGam>(x => gamIdsEnEncuestas.Contains(x.GamId));  // miembros de los gams de las encuestas

            var encuestasIds = encuestas.Results.ConvertAll(x => x.Id);
            var respuestasEncuestas = await repositorio.ConsultarAsync<RespuestaEncuestaGam>( x=> encuestasIds.Contains(x.EncuestaGamId)); // respuestas a las encuestas


            var plantillasEncuestasIds = encuestas.Results.ConvertAll(x => x.PlantillaEncuestaId).Distinct();
            var plantillasEncuestas = await repositorio.ConsultarAsync<PlantillaEncuesta>(x => plantillasEncuestasIds.Contains(x.Id ));
            var preguntasPlantillas = await repositorio.ConsultarAsync<PreguntaPlantillaEncuesta>(x => plantillasEncuestasIds.Contains(x.PlantillaEncuestaId));
            
            encuestas.Results.ForEach(encuesta =>
            {
                /*miembrosGamsEnEncuestas.FindAll(m=>m.GamId==encuesta.GamId).ForEach(m =>
                {
                    var sujeto = new EncuestaGamParticpante { MiembroGam = m, Respuestas = respuestasEncuestas.FindAll(r => r.MiembroGamId == m.Id).ToList() };
                    encuesta.Participantes.Add(sujeto);
                });*/

                encuesta.Plantilla = plantillasEncuestas.FirstOrDefault(x => x.Id == encuesta.PlantillaEncuestaId) ?? new PlantillaEncuesta();
                encuesta.Preguntas = preguntasPlantillas.FindAll(x => x.PlantillaEncuestaId == encuesta.Plantilla.Id);

                miembrosGamsEnEncuestas.FindAll(m => m.GamId == encuesta.GamId).ForEach(m =>
                {
                    /* var sujeto = new EncuestaGamParticpante { MiembroGam = m, Respuestas = respuestasEncuestas.FindAll(r => r.MiembroGamId == m.Id).ToList() };*/

                    var respuestasMiembroGam = new List<RespuestaEncuestaGam>();

                    encuesta.Preguntas.ForEach(pregunta => {
                        respuestasMiembroGam
                        .Add(respuestasEncuestas.FirstOrDefault(r => r.MiembroGamId == m.Id && r.PreguntaPlantillaEncuestaId == pregunta.Id)
                        ?? new RespuestaEncuestaGam { PreguntaPlantillaEncuestaId = pregunta.Id, EncuestaGamId = encuesta.Id, MiembroGamId=m.Id,  }); 

                    });

                    var participante = new EncuestaGamParticpante
                    {
                        MiembroGam = m,
                        Respuestas = respuestasMiembroGam
                    };

                    encuesta.Participantes.Add(participante);

                });

            });

            return encuestas;
        }
                

        protected override void PreprocesarAlCrear(EncuestaGam entidad, EntidadAutoinfo<EncuestaGam> autoinfoAlCrear)
		{
			autoinfoAlCrear.SetValue(q => q.FechaCreacion, DateTime.UtcNow);
			base.PreprocesarAlCrear(entidad, autoinfoAlCrear);
		}
	}
}
