using ServiceStack;
using SeguimientoGam.Modelos.Entidades;
using System.Collections.Generic;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class EncuestaGamConsultar : QueryDb<EncuestaGam,EncuestaGamEventoCalendario>
    {
    }


    public class EncuestaGamRespuestasConsultar : QueryDb<EncuestaGam, EncuestaGamRespuestas>
    {
    }

    public class EncuestaGamRespuestas:EncuestaGam
    {
        public EncuestaGamRespuestas()
        {
            Participantes = new List<EncuestaGamParticpante>();
            Plantilla = new PlantillaEncuesta();
            Preguntas = new List<PreguntaPlantillaEncuesta>();
        }
        public List<EncuestaGamParticpante> Participantes { get; set; }
        public PlantillaEncuesta Plantilla { get; set; }
        public List<PreguntaPlantillaEncuesta> Preguntas { get; set; }
    }

    public class EncuestaGamParticpante
    {
        public EncuestaGamParticpante()
        {
            Respuestas = new List<RespuestaEncuestaGam>();
        }
        public MiembroGam MiembroGam { get; set; }
        public List<RespuestaEncuestaGam> Respuestas { get; set; }

    }

    

    public class EncuestaGamEventoCalendario : EncuestaGam
    {
        public EncuestaGamEventoCalendario()
        {
            EventoCalendario = new EventoCalendario();
        }

        public EventoCalendario EventoCalendario { get; set; }

    }


}
