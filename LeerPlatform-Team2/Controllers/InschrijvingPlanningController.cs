using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeerPlatform_Team2.Controllers
{
    public class InschrijvingPlanningController : Controller
    {
        private IInschrijvingPlanService _svc;

        public InschrijvingPlanningController(IInschrijvingPlanService svc)
        {
            _svc = svc;
        }
        public IActionResult Index()
        {
            List<TblPlanning> planningen = _svc.GetPlanList();
            List<Inschrijvingen> inschrijvingen = _svc.GetInschList();

            var output = (from p in planningen
                         join i in inschrijvingen on p.Lescode equals i.Lescode
                         where p.StartTijdstip >= DateTime.Now
                         select new InschrijvingJoinPlanning
                         {
                             inschrijving = i,
                             planning = p
                         });

            var output2 = (from p in planningen
                          join i in inschrijvingen on p.Lescode equals i.Lescode
                          select new InschrijvingJoinPlanning
                          {
                              inschrijving = i,
                              planning = p
                          });


            List<InschrijvingJoinPlanning> temp = new List<InschrijvingJoinPlanning>();

            output = output.Where(x => x.inschrijving.GebruikerNaam == User.Identity.Name);

            output2 = output2.Where(x => x.inschrijving.GebruikerNaam == User.Identity.Name);

            temp.AddRange(output);

            temp.AddRange(output2);

            List<InschrijvingJoinPlanning> def = new List<InschrijvingJoinPlanning>(); 
            
            def = temp.OrderBy(o => o.inschrijving.Lescode).ToList();

            int loop1 = 0;

            while (loop1 < 5)
            {
                for (int i = 1; i < def.Count(); i++)
                {
                    if (def[i].inschrijving.Lescode == def[i - 1].inschrijving.Lescode && def[i].planning.StartTijdstip < DateTime.Now)
                    {
                        def.RemoveAt(i);
                    }
                }
                loop1++;
            }

                

            int loop2 = 0;

            while (loop2 < 5)
            {
                for (int i = 1; i < def.Count(); i++)
                {
                    if (def[i].inschrijving.Lescode == def[i - 1].inschrijving.Lescode && def[i].planning.StartTijdstip >= def[i - 1].planning.StartTijdstip)
                    {
                        def.RemoveAt(i);
                    }
                }
                loop2++;
            }

            return View(def);
        }
    }
}