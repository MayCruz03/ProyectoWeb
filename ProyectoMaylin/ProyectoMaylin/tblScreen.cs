//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoMaylin
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblScreen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblScreen()
        {
            this.tblRestrictions = new HashSet<tblRestriction>();
        }
    
        public int scr_id { get; set; }
        public string scr_description { get; set; }
        public Nullable<bool> scr_active { get; set; }
        public string scr_controller { get; set; }
        public string scr_action { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRestriction> tblRestrictions { get; set; }
    }
}