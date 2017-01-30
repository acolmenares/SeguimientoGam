using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Aut;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Extensiones;

namespace SeguimientoGam.Gestion.Entidades
{
    public class EncuestasGam : Gestor<EncuestaGam>, IEncuestaGamGestor
    {

        readonly IEncuestaGamAlmacena repositorio;

        public EncuestasGam(IEncuestaGamAlmacena repositorio):base(repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<CrearResponse<EncuestaGamEventoCalendario>> CrearAsync(EncuestaGamCrear modelo, IRequest peticion)
        {
            var autoinfoAlCrear = new EntidadAutoinfo<EncuestaGam>();
            PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
            var encuestagam = await repositorio.CrearAsync(modelo, autoinfoAlCrear);
            return encuestagam;
        }


        public  async Task<ActualizarResponse<EncuestaGamEventoCalendario>> ActualizarAsync(EncuestaGamActualizar modelo, IRequest peticion)
        {
            var autoinfoAlActualizar = new EntidadAutoinfo<EncuestaGam>();
            PreprocesarAlActualizar(modelo, autoinfoAlActualizar, peticion);
            var encuestagam = await repositorio.ActualizarAsync(modelo, autoinfoAlActualizar);
            return encuestagam;
        }

        public async Task<QueryResponse<EncuestaGamEventoCalendario>> ConsultarAsync(EncuestaGamConsultar modelo, IRequest peticion)
        {
            var encuestas = await repositorio.ConsultarAsync(modelo, peticion.GetRequestParams() );
            return encuestas;
        }


        public async Task<QueryResponse<EncuestaGamRespuestas>> ConsultarAsync(EncuestaGamRespuestasConsultar modelo, Dictionary<string, string> peticion, Expression<Func<EncuestaGam, bool>> filtro = null)
        {
            return await repositorio.ConsultarAsync(modelo, peticion, filtro);
        }

        
        protected override void PreprocesarAlCrear(Crear<EncuestaGam> modelo, EntidadAutoinfo<EncuestaGam> autoinfoAlCrear, IRequest peticion)
        {
            var sesion = peticion.GetUserSession();
            var encuestaGamCrear = modelo as EncuestaGamCrear;

            encuestaGamCrear.PlantillaEncuestaId.SiEsCeroEntonces(() =>
            {
                encuestaGamCrear.PlantillaEncuestaId = GetPlantillaAlivioEmocionalPorDefecto(peticion).Id;
            });

            encuestaGamCrear.EncargadoId.SiEsCero_O_NullEntonces(() => encuestaGamCrear.EncargadoId = sesion.GetPersonaId());


            autoinfoAlCrear.SetValue(q => q.CreadoPorId, peticion.GetPersonaId());
            base.PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
        }

        private PlantillaEncuesta GetPlantillaAlivioEmocionalPorDefecto(IRequest peticion)
        {
            var plantillasEncuestasGestor = peticion.TryResolve<IPlantillaEncuestaGestorConsultas>();
            var alivioEmocionalCodigoEncuesta = Valores.AlivioEmocionalParametros.CodigoEncuesta;
            var plantillaPorDefecto = plantillasEncuestasGestor.ConsultarElPrimero(q => q.Codigo == alivioEmocionalCodigoEncuesta);
            return plantillaPorDefecto;
        }

        

    }
}

