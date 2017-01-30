using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeguimientoGam.Aut;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Extensiones;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;

namespace SeguimientoGam.Gestion
{
    public class Gams : Gestor<Gam>, IGamGestor
    {
        IGamAlmacena repositorio;

		public Gams(IGamAlmacena repositorio):base(repositorio)
        {
            this.repositorio = repositorio;
        }

		public async Task<QueryResponse<GamConMiembros>> ConsultarAsync(GamConMiembrosConsultar modelo, IRequest peticion)
		{
            var dynamicParams = peticion.GetRequestParams();
            peticion.GetPersonaConPerfiles().Regionales.ConvertAll(q => q.Id);
            var regionalesIdAutorizadas = peticion.GetRegionalesIdAutorizadas();

            Expression<Func<Gam, bool>> filtro = (g) => regionalesIdAutorizadas.Contains(g.RegionalId);
        
			return await repositorio.ConsultarAsync(modelo, dynamicParams, filtro);
		}


        public async Task<ActualizarResponse<GamConMiembros>> ActualizarAsync(GamActualizar modelo, IRequest peticion)
        {
            var autoinfoAlActualizar = new EntidadAutoinfo<Gam>();
            PreprocesarAlActualizar(modelo, autoinfoAlActualizar, peticion);
            var gam = await repositorio.ActualizarAsync(modelo, autoinfoAlActualizar);
            return gam;   
        }
             
        
        protected override void PreprocesarAlCrear(Crear<Gam> modelo, EntidadAutoinfo<Gam> autoinfoAlCrear, IRequest peticion)
        {
            var sesion = peticion.GetUserSession();
            var gamcrear = modelo as GamCrear;
            gamcrear.RegionalId.SiEsCeroEntonces(() => gamcrear.RegionalId = sesion.GetDefaultRegionalId());
            gamcrear.EncargadoId.SiEsCero_O_NullEntonces(() => gamcrear.EncargadoId = sesion.GetPersonaId());

            gamcrear.ProyectoId.SiEsCero_O_NullEntonces(() =>
                gamcrear.ProyectoId = Valores.AlivioEmocionalParametros.ProyectoId
            );
                        
            autoinfoAlCrear.SetValue(q => q.CreadoPorId, sesion.GetPersonaId());

            base.PreprocesarAlCrear(gamcrear, autoinfoAlCrear, peticion);
        }

        protected override void PreprocesarAlActualizar(Actualizar<Gam> modelo, EntidadAutoinfo<Gam> autoinfoAlActualizar, IRequest peticion)
        {
            var gamActualizar = modelo as GamActualizar;

            var alamcenada = GetEntityById(modelo.Id);
                            
            gamActualizar.EncargadoId.SiEsCero_O_NullEntonces(() => {
                gamActualizar.EncargadoId = alamcenada().EncargadoId;
            });

            gamActualizar.MunicipioId.SiEsCero_O_NullEntonces(() => {
                gamActualizar.MunicipioId = alamcenada().MunicipioId;
            });

            gamActualizar.Nombre.SiEstaVacia_O_NullEntonces(() => {
                gamActualizar.Nombre = alamcenada().Nombre;
            });
                                    

            base.PreprocesarAlActualizar(gamActualizar, autoinfoAlActualizar, peticion);
        }
              

        /*
        protected override void PreprocesarAlActualizar<TEntidadActualizar>(TEntidadActualizar modelo, EntidadAutoinfo<Gam> autoinfoAlActualizar)
        {
            base.PreprocesarAlActualizar<TEntidadActualizar>(modelo, autoinfoAlActualizar);
        }
        */

    }
}

