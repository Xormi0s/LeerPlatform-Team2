using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class InschrijvingPlanService : IInschrijvingPlanService
    {
        private GIPContext _ctx;

        public InschrijvingPlanService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public List<Inschrijvingen> GetInschList()
        {
            List<Inschrijvingen> inschrijving = _ctx.Inschrijvingen.ToList();
            return inschrijving;
        }

        public List<TblPlanning> GetPlanList()
        {
            List<TblPlanning> planning = _ctx.TblPlanning.ToList();
            return planning;
        }
    }
}
