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
    
    public partial class tblRestriction
    {
        public int res_id { get; set; }
        public Nullable<int> res_ScreenID { get; set; }
        public Nullable<int> res_RolID { get; set; }
    
        public virtual tblScreen tblScreen { get; set; }
        public virtual tblRol tblRol { get; set; }
    }
}
