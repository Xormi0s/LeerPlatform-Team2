using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace LeerPlatform_Team2.Services
{
    public class InschrijvingService : IInschrijvingService
    {
        private GIPContext _ctx;
        public InschrijvingService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public int Aantal(string lescode)
        {
            var QueryIngeschreven = (from s in _ctx.StudentenPerPlannings
                                     where s.Lescode == lescode
                                     select s);
            return QueryIngeschreven.Count();
        }

        public int Capaciteit(string lescode)
        {
             var QueryCapaciteit = (from p in _ctx.TblPlanning
                                    from l in _ctx.TblLokalen
                                    where p.Lokaalnummer == l.Lokaalnummer
                                    where p.Lescode == lescode
                                    select l.Capaciteit).Min();
            return QueryCapaciteit.Value;
        }

        public List<Inschrijvingen> Controle(Inschrijvingen inschrijvingen, string lesCodeList)
        {
            var controleQ = from i in _ctx.Inschrijvingen
                            where i.Lescode == lesCodeList
                            where i.GebruikerNaam == inschrijvingen.GebruikerNaam
                            select i;
            return controleQ.ToList();
        }

        public Inschrijvingen createInsch(Inschrijvingen inschrijvingen)
        {
            _ctx.Add(inschrijvingen);
            _ctx.SaveChanges();
            return inschrijvingen;
        }

        public void CreateStudentenPerPlanning(StudentenPerPlanning studenten)
        {
            _ctx.StudentenPerPlannings.Add(studenten);
            _ctx.SaveChanges();
        }

        public void DeleteInsch(Inschrijvingen inschrijvingen)
        {
            _ctx.Remove(inschrijvingen);
            _ctx.SaveChanges();
        }

        public Inschrijvingen EditInsch(Inschrijvingen inschrijvingen)
        {
            _ctx.Update(inschrijvingen);
            _ctx.SaveChanges();
            return inschrijvingen;
        }

        public IQueryable<Inschrijvingen> getAllAcc()
        {
           var outp = from i in _ctx.Inschrijvingen
                     where (i.Status == Status.geaccepteerd)
                     select new Inschrijvingen
                     {
                         InschrijvingId = i.InschrijvingId,
                         GebruikerNaam = i.GebruikerNaam,
                         Lescode = i.Lescode,
                         GebruikerNavigation = i.GebruikerNavigation
                     };
            return outp;
        }

        public IQueryable<string> getAllLescodes()
        {
            var output = from l in _ctx.TblLessen
                         select l.Lescode;
            return output;

        }

        public IQueryable<Inschrijvingen> getAllVerwerking()
        {

            var output = (from c in _ctx.Inschrijvingen
                                    where c.Status == Status.verwerking
                                    select new Inschrijvingen
                                    {
                                        InschrijvingId = c.InschrijvingId,
                                        GebruikerNaam = c.GebruikerNaam,
                                        Lescode = c.Lescode,
                                        GebruikerNavigation = c.GebruikerNavigation,
                                        LescodeNavigation = c.LescodeNavigation
                                    });
            return output;
        }

        public int? getCapaciteit(Inschrijvingen inschrijvingen)
        {
            var cap = (from p in _ctx.TblPlanning
                       from l in _ctx.TblLokalen
                       where p.Lokaalnummer == l.Lokaalnummer
                       where p.Lescode == inschrijvingen.Lescode
                       select l.Capaciteit).Min();
            return cap;
        }

        public IQueryable<StudentenPerPlanning> getIngeschreven(Inschrijvingen inschrijvingen)
        {
            var insch = (from s in _ctx.StudentenPerPlannings
                        where s.Lescode == inschrijvingen.Lescode
                        select s);
            return insch;
        }

        public async Task<Inschrijvingen> getInschrijving(int? id)
        {
            return await _ctx.Inschrijvingen.FirstOrDefaultAsync(m => m.InschrijvingId == id);
        }

        public IEnumerable<TblLessen> getLessen()
        {
            var result = from l in _ctx.TblLessen
                         select l;
            return result;
        }

        public string Lescode(int? id)
        {
            var QueryLescode = from i in _ctx.Inschrijvingen
                               where i.InschrijvingId == id
                               select i.Lescode;
            return QueryLescode.First();
        }

        public IQueryable<Inschrijvingen> SearchAccepted(string keyword)
        {
            var output = (from i in _ctx.Inschrijvingen
                          where i.GebruikerNaam.ToLower().Contains(keyword.ToLower()) ||
                          i.GebruikerNavigation.Voornaam.ToLower().Contains(keyword.ToLower()) ||
                          i.GebruikerNavigation.Achternaam.ToLower().Contains(keyword.ToLower()) ||
                          i.Lescode.ToLower().Contains(keyword.ToLower())
                          where i.Status == Status.geaccepteerd
                          select new Inschrijvingen
                          {
                              InschrijvingId = i.InschrijvingId,
                              GebruikerNaam = i.GebruikerNaam,
                              Lescode = i.Lescode,
                              GebruikerNavigation = i.GebruikerNavigation,
                              LescodeNavigation = i.LescodeNavigation
                          });
            return output;
        }

        public IQueryable<Inschrijvingen> SearchVerwerking(string keyword)
        {
            var output = (from i in _ctx.Inschrijvingen
                          where i.GebruikerNaam.ToLower().Contains(keyword.ToLower()) ||
                          i.GebruikerNavigation.Voornaam.ToLower().Contains(keyword.ToLower()) ||
                          i.GebruikerNavigation.Achternaam.ToLower().Contains(keyword.ToLower()) ||
                          i.Lescode.ToLower().Contains(keyword.ToLower())
                          where i.Status == Status.verwerking
                          select new Inschrijvingen
                          {
                              InschrijvingId = i.InschrijvingId,
                              GebruikerNaam = i.GebruikerNaam,
                              Lescode = i.Lescode,
                              GebruikerNavigation = i.GebruikerNavigation,
                              LescodeNavigation = i.LescodeNavigation
                          });
            return output;
        }

        public string User(int inschrijvingId)
        {
            var user = (from i in _ctx.Inschrijvingen
                        where i.InschrijvingId == inschrijvingId
                        select i.GebruikerNavigationId).Single();
            return user;
        }
    }
}
