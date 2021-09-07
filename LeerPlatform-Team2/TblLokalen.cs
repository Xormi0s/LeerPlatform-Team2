using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace LeerPlatform_Team2
{
    public partial class TblLokalen
    {
        
        public TblLokalen()
        {
            TblPlanning = new HashSet<TblPlanning>();
        }
       
        [Remote(action: "Validatelokaal", controller:"Lokalen",HttpMethod ="POST", ErrorMessage ="Dit lokaal bestaat al!")]
        [Key]
        public string Lokaalnummer { get; set; }
        public string Locatie { get; set; }
        public int? Capaciteit { get; set; }
       
        public int? FunctionaliteitenId { get; set; }
       

        public virtual TblFunctionaliteiten Functionaliteiten { get; set; }
        public virtual ICollection<TblPlanning> TblPlanning { get; set; }
    }
}
