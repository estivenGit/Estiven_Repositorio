//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccesoDatos.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class UsuariosTokens
    {
        [Key]
        public int id_Tokens { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public string Token { get; set; }
        public Nullable<int> tiempoMinutos { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}