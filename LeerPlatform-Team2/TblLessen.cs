using LeerPlatform_Team2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeerPlatform_Team2
{
    public partial class TblLessen
    {
        public TblLessen()
        {
            TblPlanning = new HashSet<TblPlanning>();
        }
        [MaxLength(5)]
        [Required]
        [Key]
        public string Lescode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Titel { get; set; }
        [Required]
        [Range(4,25)]
        public int Studiepunten { get; set; }
        public string Reekscode { get; set; }
        [Required]
        [MaxLength(50)]
        public virtual ICollection<TblPlanning> TblPlanning { get; set; }
        public virtual ICollection<Inschrijvingen> Inschrijving { get; set; }
    }
}
