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
    
    public partial class campeonato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public campeonato()
        {
            this.jogo = new HashSet<jogo>();
            this.time = new HashSet<time>();
        }
    
        public int CD_CAMPEONATO { get; set; }
        public string DS_NOME { get; set; }
        public string DS_REGULAMENTO { get; set; }
        public Nullable<int> NR_ANO_TEMPORADA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<jogo> jogo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<time> time { get; set; }
    }
}