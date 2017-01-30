using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.Autenticacion
{
    [Alias("usuario_regional")]
    public class UsuarioRegional
    {
        [Alias("nombre_usuario")]
        public string NombreUsuario { get; set; }

        [Alias("idregional")]
        public int RegionalId { get; set; }

    }
}
