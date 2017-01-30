using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Modelos.Interfaces;

namespace SeguimientoGam.Modelos.Autenticacion
{
	[Alias("personal")]
	public class Persona:IEntidad
	{
		[Alias("idpersona")]
		public int Id {get;set;}

		[Alias("nombres_persona")]
		public string Nombres  {get;set;}

		[Alias("apellidos_persona")]
		public string Apellidos   {get;set;}

		[Alias("correo_electronico_persona")]
		public string Correo   {get;set;}

		[Alias("idregional")]
		public int RegionalId   {get;set;}

		[Alias("foto_persona")]
		public string Foto   {get;set;}
	}

	public class PersonaConPerfiles : Persona
	{
		public PersonaConPerfiles()
		{
			Perfiles = new List<Perfil>();
            Regionales = new List<Regional>();
		}

        public List<Perfil> Perfiles { get; set; }
        public List<Regional>  Regionales { get; set; }

	}

    [Alias("personal")]
	public class Colaborador:IEntidad
    {
        [Alias("idpersona")]
        public int Id { get; set; }

        [Alias("nombres_persona")]
        public string Nombres { get; set; }

        [Alias("apellidos_persona")]
        public string Apellidos { get; set; }
        
        [Alias("idregional")]
        public int RegionalId { get; set; }

        [Alias("foto_persona")]
        public string Foto { get; set; }
    }
		

}

