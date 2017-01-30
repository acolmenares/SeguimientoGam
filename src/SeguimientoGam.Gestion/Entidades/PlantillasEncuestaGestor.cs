using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Aut;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Modelos.Extensiones;
using System;

namespace SeguimientoGam.Gestion.Entidades
{
	public class PlantillasEncuesta : Gestor<PlantillaEncuesta>, IPlantillaEncuestaGestor
	{

		public PlantillasEncuesta(IPlantillaEncuestaAlmacena repositorio) : base(repositorio)
		{
		}

		protected override void PreprocesarAlCrear(Crear<PlantillaEncuesta> modelo, EntidadAutoinfo<PlantillaEncuesta> autoinfoAlCrear, IRequest peticion)
		{
            var entidad = modelo as PlantillaEncuestaCrear;

            entidad.Inactiva.SiEsNullEntonces(() => entidad.Inactiva = false);
            entidad.Aprobada.SiEsNullEntonces(() => entidad.Aprobada = false);
            
            autoinfoAlCrear.SetValue(q => q.CreadoPorId, peticion.GetPersonaId());
			base.PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
		}


        protected override void PreprocesarAlActualizar(Actualizar<PlantillaEncuesta> modelo, EntidadAutoinfo<PlantillaEncuesta> autoinfoAlActualizar, IRequest peticion)
        {
            var entidad = modelo as PlantillaEncuestaActualizar;
            var getAlmacenada = GetEntityById(modelo.Id);
            entidad.Inactiva.SiEsNullEntonces( ()=> entidad.Inactiva= getAlmacenada().Inactiva);
            entidad.Aprobada.SiEsNullEntonces(() => entidad.Aprobada = getAlmacenada().Aprobada);
            entidad.Descripcion.SiEstaVacia_O_NullEntonces(() => entidad.Descripcion = getAlmacenada().Descripcion);
            entidad.Nombre.SiEstaVacia_O_NullEntonces(() => entidad.Nombre = getAlmacenada().Nombre);
            
            base.PreprocesarAlActualizar(modelo, autoinfoAlActualizar, peticion);
        }


        static Func<PlantillaEncuesta> GetPlantillaEncuesta(int entidadId, IConsultaEntidad<PlantillaEncuesta> repo)
        {
            PlantillaEncuesta entidadAlmacenada = null;

            Func<PlantillaEncuesta> func = () =>
            {
                if (entidadAlmacenada == null) entidadAlmacenada = repo.ConsultarPorId(entidadId);
                return entidadAlmacenada;
            };
            return func;
        }
    }
}