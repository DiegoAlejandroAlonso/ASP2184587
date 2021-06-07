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

    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.producto_compra = new HashSet<producto_compra>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = " EL CAMPO NOMBRE ES OBLIGATORIO")]
        public string nombre { get; set; }

        [Required(ErrorMessage = " EL CAMPO PRECIO UNITARIO ES OBLIGATORIO")]
        public Nullable<int> percio_unitario { get; set; }

        [Required(ErrorMessage = " EL CAMPO  DESCRIPCION ES OBLIGATORIO")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = " EL CAMPO CANTIDAD ES OBLIGATORIO")]
        public Nullable<int> cantidad { get; set; }

        [Required(ErrorMessage = " EL CAMPO ID PROVEEDOR ES OBLIGATORIO")]
        public Nullable<int> id_proveedor { get; set; }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        
        public virtual ICollection<producto_compra> producto_compra { get; set; }

        
        public virtual proveedor proveedor { get; set; }

        
    }
}
