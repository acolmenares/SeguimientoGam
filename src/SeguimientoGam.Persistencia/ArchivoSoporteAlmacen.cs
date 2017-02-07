using SeguimientoGam.Modelos.InterfacesPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeguimientoGam.Modelos.Entidades;
using System.IO;
using ServiceStack;

namespace SeguimientoGam.Persistencia
{
    public class ArchivoSoporteAlmacen : IArchivoSoporteAlmacena
    {
        private string rutaSoportes;

        string directorioSoportes = "soportes";

        DirectoryInfo dirInfoSoportes;

        public string  NombreSoporte (Soporte modelo)
        {
             return "{0}_{1}".Fmt(modelo.Id, modelo.Nombre);
        }

        public string RutaSoporte (Soporte modelo)
        {
             return ""; 
        }

        public ArchivoSoporteAlmacen(string rutaSoportes)
        {
            this.rutaSoportes = rutaSoportes;

            dirInfoSoportes=  Directory.CreateDirectory(Path.Combine(rutaSoportes, directorioSoportes));
        }

        public Task<long> CrearAsync(Soporte modelo, Stream filestream)
        {
            
            var subdirinfo = dirInfoSoportes.CreateSubdirectory("{0}_{1}".Fmt(modelo.ProyectoId, modelo.RegionalId) );

            var filePath = Path.Combine(subdirinfo.FullName, NombreSoporte(modelo));

            long bytes = 0;
            using (var fs = File.OpenWrite(filePath))
            {
                bytes =filestream.WriteTo(fs); 
            }

            //soportes/14_4/430_

            return bytes.InTask();
        }




    }
}
