using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class LessenService : ILessenService
    {
        private GIPContext _ctx;

        public LessenService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public TblLessen CreateLes(TblLessen les)
        {
            _ctx.Add(les);
            _ctx.SaveChanges();
            return les;
        }

        public void DeleteLes(TblLessen les)
        {
            _ctx.Remove(les);
            _ctx.SaveChanges();
        }

        public TblLessen EditLes(TblLessen les)
        {
            _ctx.Update(les);
            _ctx.SaveChanges();
            return les;
        }

        public IQueryable<TblLessen> GetAllLessen()
        {
            var lessen = from l in _ctx.TblLessen
                         select l;
            return lessen;
        }

        public TblLessen GetLes(string id)
        {
            var les = from l in _ctx.TblLessen
                      where l.Lescode.Equals(id)
                      select l;
            return les.FirstOrDefault();
        }

        public IQueryable<TblLessen> SearchLes(string searchValue)
        {
            var lessenData = (from c in _ctx.TblLessen
                          where c.Lescode.ToLower().Contains(searchValue.ToLower()) ||
                          c.Titel.ToLower().Contains(searchValue.ToLower()) ||
                          c.Studiepunten.ToString().Contains(searchValue.ToLower())
                          select c);
            return lessenData;
        }
    }
}
