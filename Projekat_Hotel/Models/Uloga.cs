
namespace Projekat_Hotel.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Uloga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uloga()
        {
            this.Radniks = new HashSet<Radnik>();
        }

        public int UlogaID { get; set; }
        public string NazivUloge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Radnik> Radniks { get; set; }
    }
}
