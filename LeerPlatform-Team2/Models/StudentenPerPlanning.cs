using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Models
{
    public class StudentenPerPlanning
    {
        [Key]
        public int PlanningStudentID { get; set; }
        
        public string Gebruikersnaam { get; set; }

        public string Lescode { get; set; }

        public int PlanningID { get; set; }


        public virtual TblGebruiker GebruikerNavigation { get; set; }
        public virtual TblPlanning PlanningIDNavigation { get; set; }
    }
}
