using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeerPlatform_Team2
{
    public partial class TblFunctionaliteiten
    {
        public TblFunctionaliteiten()
        {
            TblLokalen = new HashSet<TblLokalen>();
        }

        [Key]
        public int FunctionaliteitId { get; set; }
        public string Beschrijving { get; set; }

        public virtual ICollection<TblLokalen> TblLokalen { get; set; }
    }
}
