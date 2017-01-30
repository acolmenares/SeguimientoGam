using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Aut;

namespace SeguimientoGam.Gestion.Entidades
{
    public class MiembrosGam : Gestor<MiembroGam>, IMiembroGamGestor
    {
        
        public MiembrosGam(IMiembroGamAlmacena repositorio):base(repositorio)
        {    
        }
        
		protected override void PreprocesarAlCrear(Crear<MiembroGam> modelo, EntidadAutoinfo<MiembroGam> autoinfoAlCrear, IRequest peticion)
        {
            autoinfoAlCrear.SetValue(q => q.CreadoPorId, peticion.GetPersonaId());
            base.PreprocesarAlCrear(modelo, autoinfoAlCrear, peticion);
        }
                        

    }
}
