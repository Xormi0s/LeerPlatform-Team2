using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Models
{
    public class InschrijvingJoinPlanning
    {
        public Inschrijvingen inschrijving { get; set; }

        public TblPlanning planning { get; set; }

    }
}
