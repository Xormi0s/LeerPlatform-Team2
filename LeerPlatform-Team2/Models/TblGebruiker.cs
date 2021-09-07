using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Models
{
    public class TblGebruiker :IdentityUser
    {       
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Geboortedatum { get; set; }
        public string Adres { get; set; }
        public string UcllNummer { get; set; }
        public virtual ICollection<Inschrijvingen> Inschrijving { get; set; }
        
    }
}
