using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Servicios
{

    [Authenticate]
    public class SoporteService:Service
    {

        public ISoporteGestor Gestor { get; set; }

        public void Post(SoporteCrear peticion)
        {
             var r=  Gestor.CrearAsync(peticion).Result;
            //return r ; //  vaadin-upload : arroja error cuando se devulve cualuqier cosa
            
        }

        public QueryResponse<Soporte> Get(SoporteConsultar peticion)
        {
            return Gestor.ConsultarAsync(peticion, Request).Result;
        }

    }

    
}
