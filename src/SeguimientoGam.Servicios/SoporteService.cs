using SeguimientoGam.Modelos.Peticiones;
using ServiceStack;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Servicios
{
    public class SoporteService:Service
    {

        public void Post(SoporteCrear peticion)
        {
            var filePath  = System.IO.Path.Combine( @"C:\Code\tmp", peticion.EventoId+"_"+ peticion.Nombre?? Guid.NewGuid().ToString("n"));

            Request.GetRequestParams();
            /*
            foreach (var uploadedFile in Request.Files)
            {
                uploadedFile.SaveTo(filePath);
            }
            */
            Console.WriteLine(Request);

            using (var fs = File.OpenWrite(filePath))
            {
                peticion.RequestStream.WriteTo(fs); 
            }
            MimeTypes.GetMimeType("");
            
            Console.WriteLine(peticion.EventoId);

        }
    }

    
}
