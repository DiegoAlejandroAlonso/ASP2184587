//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP2184587.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class producto_compra
    {
        public int id { get; set; }
        [Required(ErrorMessage = " EL CAMPO COMPRA ES OBLIGATORIO")]
        public Nullable<int> id_compra { get; set; }
        [Required(ErrorMessage = " EL CAMPO PRODUCTO ES OBLIGATORIO")]
        public Nullable<int> id_producto { get; set; }

        [Required(ErrorMessage = " EL CAMPO CANTIDAD ES OBLIGATORIO")]
        public Nullable<int> cantidad { get; set; }

        
        public virtual compra compra { get; set; }

        
        public virtual producto producto { get; set; }

        
    }
}
