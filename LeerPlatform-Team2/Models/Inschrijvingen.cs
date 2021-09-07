using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Models
{
    public class Inschrijvingen
    {
        [Key]
        public int InschrijvingId { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
        public string GebruikerNaam { get; set; }
        public string Lescode { get; set; }
        public string GebruikerNavigationId { get; set; }


        public virtual TblGebruiker GebruikerNavigation { get; set; }
        public virtual TblLessen LescodeNavigation { get; set; }

    }
    
    public enum Status
    {
        
        [Display(Name ="Verwerking")]
        verwerking = 1,
        [Display(Name = "Geaccepteerd")]
        geaccepteerd =2,
        [Display(Name = "Geweigerd")]
        geweigerd =3
    }

}
