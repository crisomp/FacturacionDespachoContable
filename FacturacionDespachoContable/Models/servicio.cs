//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FacturacionDespachoContable.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public servicio()
        {
            this.detalleCreditoFiscal = new HashSet<detalleCreditoFiscal>();
            this.detalleFactura = new HashSet<detalleFactura>();
        }
    
        public int idServicio { get; set; }

        [Required(ErrorMessage = "Requerido"), Display(Name = "Servicio"), MaxLength(50, ErrorMessage = "Maximo 50")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Requerido"), Display(Name = "Precio")]
        public decimal precio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalleCreditoFiscal> detalleCreditoFiscal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalleFactura> detalleFactura { get; set; }
    }
}
