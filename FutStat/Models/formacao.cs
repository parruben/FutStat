//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FutStat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class formacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public formacao()
        {
            this.jogo = new HashSet<jogo>();
            this.jogo1 = new HashSet<jogo>();
        }
    
        public int CD_FORMACAO { get; set; }
        public string DS_FORMACAO { get; set; }
        public int QT_ZAGUEIRO { get; set; }
        public int QT_MEIO_CAMPO { get; set; }
        public int QT_ATACANTE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jogo> jogo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jogo> jogo1 { get; set; }
    }
}
