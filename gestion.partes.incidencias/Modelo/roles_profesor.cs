//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestion.partes.incidencias.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class roles_profesor
    {
        public int id { get; set; }
        public string dni_profesor { get; set; }
        public string codigo_rol { get; set; }
    
        public virtual profesor profesor { get; set; }
        public virtual rol rol { get; set; }
    }
}