//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transversal.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LogErrores
    {
        [Key]
        public long id { get; set; }
        public string Mensaje { get; set; }
        public string Linea { get; set; }
        public string Ayuda_en_linea { get; set; }
        public string Fuente { get; set; }
        public string Metodo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    }
}
