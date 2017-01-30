using System;
using Funq;
using SeguimientoGam.Persistencia;
using SeguimientoGam.Modelos.Interfaces;
using SeguimientoGam.Modelos.Peticiones;
using SeguimientoGam.Servicios;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using SeguimientoGam.Modelos.InterfacesGestion;
using SeguimientoGam.Gestion.Entidades;
using SeguimientoGam.Modelos.InterfacesPersistencia;
using SeguimientoGam.Gestion;
using SeguimientoGam.InterfacesGestion;
using SeguimientoGam.Modelos.Entidades;
using SeguimientoGam.Aut;
using SeguimientoGam.Modelos.Autenticacion;
using ServiceStack.Text;
using ServiceStack.Validation;
using ServiceStack.Logging;

namespace SeguimientoGam.WebHost
{
    public class AppHost : AppHostBase
	{
        
        public AppHost() : base("GAM API", typeof(GamServicio).Assembly) { }

		public override void Configure(Container container)
		{
			SetConfig(new HostConfig
			{
				DebugMode = true,
				HandlerFactoryPath = "gam-api",
				GlobalResponseHeaders =
					{
						{ "Access-Control-Allow-Origin", "*" },
						{ "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
					},
				DefaultContentType = "json"
			});

			LogManager.LogFactory = new ConsoleLogFactory(debugEnabled: true);;
			ConfigureRoutes();


            RegisterTypedResponseFilter<AuthenticateResponse>((request, response, auth) =>
            {
                //Console.WriteLine("{0}. {1}. {2}", request, response, auth);
            });
            ResponseConverters.Add((request, response) =>
			{

				if (response == null || response.GetType() != typeof(AuthenticateResponse) || request.PathInfo.EndsWith("logout")) return null;

				var resp = response.GetDto<AuthenticateResponse>();

				resp.Meta = new System.Collections.Generic.Dictionary<string, string>();

				var sesion = request.SessionAs<CustomUserSession>();
				var personaConPerfiles = sesion.PersonaConPerfiles;
				var serializado = JsonSerializer.SerializeToString(personaConPerfiles);
				resp.Meta.Add("PersonaConPerfiles", serializado);

				var regionales = sesion.Regionales;
				serializado = JsonSerializer.SerializeToString(regionales);
				resp.Meta.Add("Regionales", serializado);

				var colaboradores = sesion.Colaboradores;
				serializado = JsonSerializer.SerializeToString(colaboradores);
				resp.Meta.Add("Colaboradores", serializado);


				var generos = sesion.Generos;
				if (generos.Count == 0)
				{
					generos.AddRange(new string[] { "Masculino", "Femenino" });
				}
				serializado = JsonSerializer.SerializeToString(generos);
				resp.Meta.Add("Generos", serializado);

				var tiposDocumento = sesion.TiposDocumento;
				if (tiposDocumento.Count == 0){
					tiposDocumento.AddRange(new string[] { "CC", "NUIP", "RC","TI" });
				}
				serializado = JsonSerializer.SerializeToString(tiposDocumento);
				resp.Meta.Add("TiposDocumento", serializado);


                var _valores = request.TryResolve<Valores>();

                resp.Meta.Add("AlivioEmocionalParametros", JsonSerializer.SerializeToString(_valores.AlivioEmocionalParametros));
                
                return resp;

            });

			Plugins.Add(new CorsFeature());
			Plugins.Add(new SessionFeature()); // TODO : PONER REDIS AQUI!

            Plugins.Add(new AuthFeature(
                () => new CustomUserSession(), // new AuthUserSession(),
                new IAuthProvider[] { new CustomCredentialsAuthProvider() })
            { IncludeAssignRoleServices = false });
            
			Plugins.Add(new AutoQueryFeature { MaxLimit = 100, UseNamedConnection="autoquery"  });
			Plugins.Add(new ValidationFeature());

			var appSettings = new AppSettings();
            var valores = new Valores(appSettings);

            var connectionFactory = MySqlConnectionFactory(appSettings);
            container.Register<IDbConnectionFactory>(connectionFactory);

            container.Register<Valores>( valores );


            container.Register<ICacheClient>(new MemoryCacheClient { FlushOnDispose = false });
            container.Register<IUserAuthRepository>(new InMemoryAuthRepository());

            container.Register<IProveedorHash>(new ProveedorHashMd5());

			container.Register<IAlmacenaEntidad>(
				c => new AlmacenaEntidad(
					c.Resolve<IDbConnectionFactory>(), c.Resolve<IAutoQueryDb>(), c.Resolve<ICacheClient>() ))
			         .ReusedWithin(ReuseScope.Request); // la misma de autoquery = None.

			//

			container.Register<IUsuarioGestorConsultas>
					 (c => new Usuarios(new UsuarioAlmacen(c.Resolve<IAlmacenaEntidad>()),
										c.Resolve<IProveedorHash>()))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IPersonaGestorConsultas>
					 (c => new Personas(new PersonaAlmacen(c.Resolve<IAlmacenaEntidad>())))
					 .ReusedWithin(ReuseScope.Request);
			//


			// Gestor Colaborador
            container.Register<IColaboradorGestor>
				(c => new Colaboradores (new ColaboradorAlmacen(c.Resolve<IAlmacenaEntidad>())))
				.ReusedWithin(ReuseScope.Request);

			container.Register<IColaboradorGestorConsultas>
				(c => c.Resolve<IColaboradorGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//

			// Gestor Regional
			container.Register<IRegionalGestor>
                (c => new Regionales(new RegionalAlmacen(c.Resolve<IAlmacenaEntidad>())))
                .ReusedWithin(ReuseScope.Request);

			container.Register<IRegionalGestorConsultas>
				(c => c.Resolve<IRegionalGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//


			//Gestor GAM
			container.Register<IGamGestor>
					 (c => new Gams(new GamAlmacen(c.Resolve<IAlmacenaEntidad>())) { Valores = c.Resolve<Valores>() })
					 .ReusedWithin(ReuseScope.Request);
            
            container.Register<IGamGestorConsultas>
                     (c => c.Resolve<IGamGestor>())
                     .ReusedWithin(ReuseScope.Request);
            //

            //Gestor MiembroGam
            container.Register<IMiembroGamGestor>
                     (c => new MiembrosGam(new MiembroGamAlmacen(c.Resolve<IAlmacenaEntidad>())))
                     .ReusedWithin(ReuseScope.Request);

            container.Register<IMiembroGamGestorConsultas>
                     (c => c.Resolve<IMiembroGamGestor>())
                     .ReusedWithin(ReuseScope.Request);

			//Gestor  EncuestaGam
			container.Register<IEncuestaGamGestor>
					 (c => new EncuestasGam(new EncuestaGamAlmacen(c.Resolve<IAlmacenaEntidad>())) { Valores = c.Resolve<Valores>() })
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IEncuestaGamGestorConsultas>
					 (c => c.Resolve<IEncuestaGamGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//

			// Gestor Respuesta
			container.Register<IRespuestaEncuestaGamGestor>
					 (c => new RespuestasEncuestaGam(new RespuestaEncuestaGamAlmacen(c.Resolve<IAlmacenaEntidad>())))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IRespuestaEncuestaGamGestorConsultas>
					 (c => c.Resolve<IRespuestaEncuestaGamGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//

			//Gestor  PlantillaEncuestaGam
			container.Register<IPlantillaEncuestaGestor>
					 (c => new PlantillasEncuesta(new PlantillaEncuestaAlmacen(c.Resolve<IAlmacenaEntidad>())))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IPlantillaEncuestaGestorConsultas>
					 (c => c.Resolve<IPlantillaEncuestaGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//

			//Gestor  PreguntaPlantillaEncuestaGam
			container.Register<IPreguntaPlantillaEncuestaGestor>
					 (c => new PreguntasPlantillaEncuesta(new PreguntaPlantillaEncuestaAlmacen(c.Resolve<IAlmacenaEntidad>())))
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IPreguntaPlantillaEncuestaGestorConsultas>
					 (c => c.Resolve<IPreguntaPlantillaEncuestaGestor>())
					 .ReusedWithin(ReuseScope.Request);
			//


			//Gestor EventoCalendario
			container.Register<IEventoCalendarioGestor>
					 (c => new EventosCalendario(new EventoCalendarioAlmacen(c.Resolve<IAlmacenaEntidad>())) )
					 .ReusedWithin(ReuseScope.Request);

			container.Register<IEventoCalendarioGestorConsultas>
					 (c => c.Resolve<IEventoCalendarioGestor>())
					 .ReusedWithin(ReuseScope.Request);

			container.RegisterValidators(typeof(GamCrearValidador).Assembly);

            container.Register<GamReglasValidacion>
                     (c => new GamReglasValidacion())
                     .ReusedWithin(ReuseScope.Request);

            container.Register<RespuestaEncuestaGamReglasValidacion>
                     (c => new RespuestaEncuestaGamReglasValidacion() )
                     .ReusedWithin(ReuseScope.Request);

            CrearTablasGam(connectionFactory, valores);


        }

		void ConfigureRoutes()
		{

            Routes.Add<EventoCalendarioConsultar>("/eventocalendario/consultar", ApplyTo.Get);
            Routes.Add<EncuestaGamEventoCalendarioConsultar>("/encuestagam/eventocalendario/consultar", ApplyTo.Get); 

            Routes.Add<ColaboradorConsultar>("/colaborador/consultar", ApplyTo.Get);

            Routes.Add<GamConsultar>("/gam/consultar", ApplyTo.Get);
            Routes.Add<GamCrear>("/gam/crear", ApplyTo.Post);
			Routes.Add<GamActualizar>("/gam/actualizar", ApplyTo.Post);
            Routes.Add<GamBorrar>("/gam/borrar", ApplyTo.Post);


            Routes.Add<MiembroGamConsultar>("/miembrogam/consultar", ApplyTo.Get);
            Routes.Add<MiembroGamCrear>("/miembrogam/crear", ApplyTo.Post);
            Routes.Add<MiembroGamActualizar>("/miembrogam/actualizar", ApplyTo.Post);
            Routes.Add<MiembroGamBorrar>("/miembrogam/borrar", ApplyTo.Post);

			Routes.Add<GamConMiembrosConsultar>("/gam/miembros/consultar", ApplyTo.Get);

            Routes.Add<EncuestaGamConsultar>("/encuestagam/consultar", ApplyTo.Get);
			Routes.Add<EncuestaGamCrear>("/encuestagam/crear", ApplyTo.Post);
			Routes.Add<EncuestaGamActualizar>("/encuestagam/actualizar", ApplyTo.Post);
			Routes.Add<EncuestaGamBorrar>("/encuestagam/borrar", ApplyTo.Post);

            Routes.Add<EncuestaGamRespuestasConsultar>("/encuestagam/respuestas/consultar", ApplyTo.Get);


            Routes.Add<RespuestaEncuestaGamConsultar>("/respuestaencuestagam/consultar", ApplyTo.Get);
			Routes.Add<RespuestaEncuestaGamCrear>("/respuestaencuestagam/crear", ApplyTo.Post);
			Routes.Add<RespuestaEncuestaGamActualizar>("/respuestaencuestagam/actualizar", ApplyTo.Post);
			Routes.Add<RespuestaEncuestaGamBorrar>("/respuestaencuestagam/borrar", ApplyTo.Post);

			Routes.Add<PlantillaEncuestaConsultar>("/plantillaencuesta/consultar", ApplyTo.Get);
			Routes.Add<PlantillaEncuestaCrear>("/plantillaencuesta/crear", ApplyTo.Post);
			Routes.Add<PlantillaEncuestaActualizar>("/plantillaencuesta/actualizar", ApplyTo.Post);
			Routes.Add<PlantillaEncuestaBorrar>("/plantillaencuesta/borrar", ApplyTo.Post);

            Routes.Add<RegionalConMunicipiosConsultar>("/regionalconmunicipios/consultar", ApplyTo.Get);
			Routes.Add<RegionalConsultar>("/regional/consultar", ApplyTo.Get);
			Routes.Add<RegionalCrear>("/regional/crear", ApplyTo.Post);
			Routes.Add<Authenticate>("/login", ApplyTo.All);

        }

		static OrmLiteConnectionFactory MySqlConnectionFactory(AppSettings appSettings)
		{
			var bdGam = appSettings.Get("CONEXION_BD_GAM", Environment.GetEnvironmentVariable("CONEXION_BD_GAM"));

			var dbfactory = new OrmLiteConnectionFactory(bdGam,MySqlDialect.Provider)
			{
				ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
			};
			dbfactory.RegisterConnection ("autoquery", bdGam, MySqlDialect.Provider);
			return dbfactory;
		}


		static void CrearTablasGam(OrmLiteConnectionFactory dbfactory, Valores valores)
		{
            var overwrite = false;
            using (var con = dbfactory.Open()) {

                con.CreateTable<PersonaRegional>(overwrite);
                con.CreateTable<Gam>(overwrite);
                con.CreateTable<MiembroGam>(overwrite);
                con.CreateTable<PlantillaEncuesta>();
                con.CreateTable<PreguntaPlantillaEncuesta>(overwrite);
                con.CreateTable<EncuestaGam>(overwrite);
                con.CreateTable<RespuestaEncuestaGam>(overwrite);

                var codigoEncuestaAlivioEmocional = valores.AlivioEmocionalParametros.CodigoEncuesta;
                var plantillaEncuestaAlivioEmocional = con.Single<PlantillaEncuesta>(q => q.Codigo == codigoEncuestaAlivioEmocional) ?? new PlantillaEncuesta();
                if (plantillaEncuestaAlivioEmocional.Id == 0)
                {
                    plantillaEncuestaAlivioEmocional.Codigo = codigoEncuestaAlivioEmocional;
                    plantillaEncuestaAlivioEmocional.Inactiva = false;
                    plantillaEncuestaAlivioEmocional.Aprobada = true;
                    plantillaEncuestaAlivioEmocional.Nombre = "Alivio Emocional";
                    plantillaEncuestaAlivioEmocional.Descripcion = "Encuesta para el seguimiento del alivio emocional participantes GAM";
                    plantillaEncuestaAlivioEmocional.FechaCreacion = DateTime.UtcNow;
                    plantillaEncuestaAlivioEmocional.CreadoPorId = 28;

					try
					{
						var idPlantillaEncuesta = con.Insert(plantillaEncuestaAlivioEmocional, selectIdentity: true);

						var codigoPreguntaComoMeSiento = valores.AlivioEmocionalParametros.CodigoEPreguntaComoMeSiento;
						var pregunta = new PreguntaPlantillaEncuesta
						{
							PlantillaEncuestaId = (int)idPlantillaEncuesta,
							Codigo = codigoPreguntaComoMeSiento,
							Texto = "Al día de hoy como considera que se encuentra dentro del proceso de Alivio Emocional",
							TipoRespuesta = "Texto",
							FechaCreacion = DateTime.UtcNow,
							CreadoPorId = 28
						};

						con.Insert(pregunta, selectIdentity: true);
					}
					catch (Exception ex) {
						Console.WriteLine(ex);
					}
                }
            }

            
		}

	}
}

/*
 * LineaDeAccion = > Id=15      Codigo 4.2 Nombre Grupo de apoyo mutuo, LineaObjectivoId= 19
 *   *** con la lineadeAccion puede ir Objetivo y capturar el ProyectoID!!!
 * Guardar AlivioEmocionalLineaDeAccionId = 15,  AlivioEmocionalProyectoId=14 
 * 
 * MacroActividad => Filtrar por Proyecto, Regionales del usuario y LineaccionId
 * EventoMacroActividad = > Filtrar por los Id de MacroActividad el punto anterior
 * Eventos => Filtar por los id de los enventos del punto anterior y PersonaID y realizacion = Programada
 * 
 * SELECT * 
FROM `events` 
join evento_macroactividad on evento_macroactividad.idevento= events.id
join macroactividad on macroactividad.idmacroactividad= evento_macroactividad.idmacroactividad
join lineaaccion on lineaaccion.idobjetivo= macroactividad.idobjetivo
WHERE 
events.idregional=10 
and events.idproyecto=14 
and events.realizacion='Programada' 
and events.idpersona=27 
and lineaaccion.idlineaaccion=15
 */
