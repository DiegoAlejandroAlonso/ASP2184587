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

    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.compra = new HashSet<compra>();
        }
    
        public int id { get; set; }

        [Required (ErrorMessage =  " NO PUEDE IR VACIO ")]
        [StringLength(10,  ErrorMessage = "EL CAMPO NOMBRE ES REQUERIDO")   ]
        public string nombre { get; set; }

        [Required(ErrorMessage = " EL CAMPO APELLIDO ES OBLIGATORIO")]
        public string apellido { get; set; }

        [Required(ErrorMessage = " EL CAMPO FECHA NACIMIENTO ES OBLIGATORIO")]
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = " EL CAMPO EMAIL ES OBLIGATORIO ")]
        public string email { get; set; }


        [Required(ErrorMessage = " EL CAMPO CONTRASEÑA ES OBLIGATORIO")]
        public string password { get; set; }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compra> compra { get; set; }

        
    }
}
