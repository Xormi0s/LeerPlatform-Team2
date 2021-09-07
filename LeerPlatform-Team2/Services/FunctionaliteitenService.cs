using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class FunctionaliteitenService : IFunctionaliteitenService
    {
        private GIPContext _ctx;
        public FunctionaliteitenService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public TblFunctionaliteiten CreateFunctionaliteit(TblFunctionaliteiten functie)
        {
            _ctx.Add(functie);
            _ctx.SaveChanges();
            return functie;
        }

        public void DeleteFunctionaliteit(TblFunctionaliteiten functie)
        {
            _ctx.Remove(functie);
            _ctx.SaveChanges();
        }

        public TblFunctionaliteiten EditFunctionaliteit(TblFunctionaliteiten functie)
        {
            _ctx.Update(functie);
            _ctx.SaveChanges();
            return functie;
        }

        public IEnumerable<TblFunctionaliteiten> GetAllFuncties()
        {
            var result = from f in _ctx.TblFunctionaliteiten
                         select f;
            return result;
        }

        public TblFunctionaliteiten GetFunctionaliteit(int? id)
        {
            var result = from f in _ctx.TblFunctionaliteiten
                         where f.FunctionaliteitId.Equals(id)
                         select f;
            return result.FirstOrDefault();
        }
    }
}
