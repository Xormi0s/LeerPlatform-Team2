using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeerPlatform_Team2
{
    public partial class TblLessenreeks
    {
        public TblLessenreeks()
        {
            TblPlanning = new HashSet<TblPlanning>();
        }

        [Key]
        public string Reekscode { get; set; }
        public string Titel { get; set; }
        public int? Ingeschreven { get; set; }

        public virtual ICollection<TblPlanning> TblPlanning { get; set; }
    }
}
