using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeerPlatform_Team2
{
    public partial class TblPlanning
    {
        [Key]
        public int PlanningId { get; set; }
        public string Lokaalnummer { get; set; }
        public string Lescode { get; set; }
        public string Reekscode { get; set; }
        [DisplayName("Start tijdstip")]
        [Required(ErrorMessage = "Een start tijdstip is verplicht in te vullen !")]
        public DateTime StartTijdstip { get; set; }
        [DisplayName("Eind tijdstip")]
        [Required(ErrorMessage = "Een eind tijdstip is verplicht in te vullen !")]
        public DateTime EindTijdstip { get; set; }
        [DisplayName("Extra info")]
        public string ExtraInfo { get; set; }

        public virtual TblLessen LescodeNavigation { get; set; }
        public virtual TblLokalen LokaalnummerNavigation { get; set; }
        public virtual TblLessenreeks ReekscodeNavigation { get; set; }

    }
}
