using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Modelos.Autenticacion
{
    [Alias("persona_regional")]
    public class PersonaRegional
    {
        [Alias("idpersona")]
        public int PersonaId { get; set; }

        [Alias("idregional")]
        public int RegionalId { get; set; }

    }
}
