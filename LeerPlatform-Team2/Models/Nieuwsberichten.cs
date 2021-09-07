using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Models
{
    public class Nieuwsberichten
    {
        [Key]
        public int BerichtenID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Datum { get; set;}

        public string Titel { get; set; }

        public string Bericht { get; set; }

    }
}
