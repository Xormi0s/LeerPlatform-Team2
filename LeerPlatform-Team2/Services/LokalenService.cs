using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class LokalenService : ILokalenService
    {
        private GIPContext _ctx;

        public LokalenService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public TblLokalen CreateLokaal(TblLokalen lokaal)
        {
            _ctx.Add(lokaal);
            _ctx.SaveChanges();
            return lokaal;
        }

        public void DeleteLokaal(TblLokalen lokaal)
        {
            _ctx.Remove(lokaal);
            _ctx.SaveChanges();
        }

        public TblLokalen EditLokaal(TblLokalen lokaal)
        {
            _ctx.Update(lokaal);
            _ctx.SaveChanges();
            return lokaal;
        }

        public List<TblLokalen> GetAll()
        {
            var result = from l in _ctx.TblLokalen
                         select l;
            return result.ToList();
        }

        public IQueryable<TblLokalen> GetAllLokalen()
        {
            var result = from l in _ctx.TblLokalen
                         select new TblLokalen
                         {
                             Capaciteit = l.Capaciteit,
                             Functionaliteiten = l.Functionaliteiten,
                             Locatie = l.Locatie,
                             Lokaalnummer = l.Lokaalnummer
                         };
            return result;
        }

        public IEnumerable<TblFunctionaliteiten> GetFunct()
        {
            var result = from f in _ctx.TblFunctionaliteiten
                         select f;
            return result;
        }

        public IIncludableQueryable<TblLokalen, TblFunctionaliteiten> GetIndex()
        {
            var result = _ctx.TblLokalen.Include(f => f.Functionaliteiten);
            return result;
        }

        public TblLokalen GetLokaal(string lokaalNummer)
        {
            var result = from c in _ctx.TblLokalen
                         where c.Lokaalnummer.Equals(lokaalNummer)
                         select c;
            return result.FirstOrDefault();
        }

        public IQueryable<string> GetLokaalNummers()
        {
            var result = from l in _ctx.TblLokalen
                         select l.Lokaalnummer;
            return result;
        }

        public IQueryable<TblLokalen> SearchLokalen(string keyword)
        {
            var result = from c in _ctx.TblLokalen
                         where c.Lokaalnummer.ToLower().Contains(keyword.ToLower()) ||
                         c.Locatie.ToLower().Contains(keyword.ToLower()) ||
                         c.Capaciteit.ToString().Contains(keyword.ToLower()) ||
                         c.Functionaliteiten.Beschrijving.ToLower().Contains(keyword.ToLower())
                         select new TblLokalen
                         {
                             Capaciteit = c.Capaciteit,
                             Functionaliteiten = c.Functionaliteiten,
                             Locatie = c.Locatie,
                             Lokaalnummer = c.Lokaalnummer
                         };
            return result;
        }
    }
}
