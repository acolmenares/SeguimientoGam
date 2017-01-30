using ServiceStack.FluentValidation;
using SeguimientoGam.Modelos.Peticiones;

namespace SeguimientoGam.Aut
{
    public class GamCrearValidador :AbstractValidator<GamCrear>
    {
		public GamCrearValidador()
		{

                                    
			RuleFor(x => x.MunicipioId)
				.Must((e, idMunicipio) => idMunicipio != 0)
				.WithMessage("Debe indicar el Municipio del GAM");

            RuleFor(x => x.RegionalId)
				.Must((e, idRegional) => ValidarEsRegionalAutorizada(e, idRegional))
				.WithMessage("{Usuario No autorizado para la Regional con Id: {0}", e => e.RegionalId);

            RuleFor(x => x.Nombre)
				.NotEmpty()
				.WithMessage("Debe indicar el nombre del GAM");
            

        }

		bool ValidarEsRegionalAutorizada(GamCrear e, int idRegional)
		{
			return idRegional == 0 || Request.EsRegionalAutorizada(idRegional); 
		}

        


    }
}

