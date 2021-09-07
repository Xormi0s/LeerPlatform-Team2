using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LeerPlatform_Team2.Services
{
    public class PlanningService : IPlanningService
    {
        private GIPContext _ctx;

        public PlanningService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public TblPlanning CreatePlan(TblPlanning planning)
        {
            _ctx.Add(planning);
            _ctx.SaveChanges();
            return planning;
        }

        public void DeletePlanning(TblPlanning planning)
        {
            _ctx.Remove(planning);
            _ctx.SaveChanges();
        }

        public TblPlanning EditPlanning(TblPlanning planning)
        {
            _ctx.Update(planning);
            _ctx.SaveChanges();
            return planning;
        }

        public IQueryable<TblPlanning> GetAllPlanning()
        {
            var result = from r in _ctx.TblPlanning
                         select r;
            return result;
        }

        public IIncludableQueryable<TblPlanning, TblLessenreeks> GetIndex()
        {
            var result = _ctx.TblPlanning.Include(t => t.LescodeNavigation).Include(t => t.LokaalnummerNavigation).Include(t => t.ReekscodeNavigation);
            return result;
        }

        public IEnumerable<TblLessen> Getlessen()
        {
            var result = from l in _ctx.TblLessen
                         select l;
            return result;
        }

        public IEnumerable<TblLessenreeks> GetLessenreeks()
        {
            var result = from l in _ctx.TblLessenreeks
                         select l;
            return result;
        }

        public IEnumerable<TblLokalen> Getlokaal()
        {
            var result = from l in _ctx.TblLokalen
                         select l;
            return result;
        }

        public IQueryable<string> GetLokaalNummer(TblPlanning planning)
        {
            var result = from p in _ctx.TblPlanning
                         where p.StartTijdstip.Month == planning.StartTijdstip.Month && p.StartTijdstip.Day == planning.StartTijdstip.Day
                         select p.Lokaalnummer;
            return result;
        }

        public TblPlanning Getplanning(int? id)
        {
            var result=from p in _ctx.TblPlanning
                       where p.PlanningId.Equals(id)
                       select p;
            return result.FirstOrDefault();
        }

        public IQueryable<TblPlanning> SearchPlanning(string keyword)
        {
            String date = keyword.Substring(1).ToLower();
            var result =(from c in _ctx.TblPlanning
                         where c.Lokaalnummer.ToLower().Contains(keyword.ToLower()) ||
                         c.Lescode.ToLower().Contains(keyword.ToLower()) ||
                         // Onderste 2 lijntjes door Jonas bijgevoegd. Filteren op uur niet nodig ? Enkel momenteel op dag en maand.
                         c.StartTijdstip.Day.ToString().Contains(date) ||
                         c.StartTijdstip.Month.ToString().Contains(date) ||
                         //c.StartTijdstip.Hour.ToString().Contains(keyword.ToLower()) ||
                         c.Reekscode.ToLower().Contains(keyword.ToLower()) ||
                         c.ExtraInfo.ToLower().Contains(keyword.ToLower())
                         select c);
            return result;
        }
    }
}



