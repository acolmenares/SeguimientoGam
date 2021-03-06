﻿using SeguimientoGam.Modelos.Entidades;
using ServiceStack;
using System;

namespace SeguimientoGam.Modelos.Peticiones
{
    public class EventoCalendarioConsultar:QueryDb<EventoCalendario>{
               
    }


    public class EncuestaGamEventoCalendarioConsultar : QueryDb<EventoCalendario>,
        IJoin<EventoCalendario, EventoMacroActividad>,
        IJoin<EventoMacroActividad, MacroActividad>,
        IJoin<MacroActividad, LineaDeAccion>    {

	
        public int? LineaDeAccionId { get; set; }
        public int? ProyectoId { get; set; }
        public int? RegionalId { get; set; }
        public int? EncargadoId { get; set; }
        public string Realizacion { get; set; }
        //public DateTime? FechaGreaterThanOrEqualTo { get; set; }

        [QueryDbField(Template = "Date({Field})={Value}", Field = "date")]
        public DateTime? Fecha { get; set; }

    }
    /*
    SELECT e.* 
     FROM `events` e 
        join evento_macroactividad em on em.idevento= e.id 
        join macroactividad ma on ma.idmacroactividad= em.idmacroactividad 
        join lineaaccion la on la.idlineaaccion= ma.idlineaaccion 
     WHERE la.idlineaaccion= 15 ORDER BY `e`.`id` DESC
     */
}

