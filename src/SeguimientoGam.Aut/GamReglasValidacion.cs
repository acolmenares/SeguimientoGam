using System;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.InterfacesGestion;
using ServiceStack.Web;
using SeguimientoGam.Modelos.Extensiones;
using System.Threading.Tasks;

namespace SeguimientoGam.Aut
{
    public class GamReglasValidacion
    {
        //public IGamGestorConsultas Gam {  get; set; }

        public async Task<bool> ValidarEsPropietarioEncargadoAdminColaborardor(int gamId, IRequest peticion)
        {
            var gamAlmacenado = await ObeteneGamPorId(gamId, peticion);
            return EsPropietarioEndargadoAdminColaborador(gamAlmacenado, peticion);
        }

        public async Task<bool>  ValidarCambioDelEncargado(int gamId,  int? encargadoId, IRequest peticion)
        {
            var gamAlmacenado = await ObeteneGamPorId(gamId, peticion);
            return encargadoId.EsCero_O_Null() || EsPropietario_O_Admin(gamAlmacenado, peticion);

        }

        private bool EsPropietario_O_Admin(Gam gamAlmacenado, IRequest peticion)
        {
            var sesionn = peticion.GetUserSession();
            return gamAlmacenado.CreadoPorId == sesionn.GetPersonaId()   || sesionn.HasAdminRole(peticion); ;
        }

        private bool EsPropietarioEndargadoAdminColaborador(Gam gamAlmacenado, IRequest peticion)
        {
            var sesionn = peticion.GetUserSession();
            return gamAlmacenado.CreadoPorId == sesionn.GetPersonaId()  || gamAlmacenado.EncargadoId== sesionn.GetPersonaId()
                || sesionn.HasAdminRole(peticion); ;
        }


        private async Task<Gam> ObeteneGamPorId(int gamiId, IRequest peticion)
        {                                  
            var gestor = peticion.TryResolve<IGamGestorConsultas>();
            return await gestor.ConsultarPorIdAsync(gamiId);
         }

        
    }
}
