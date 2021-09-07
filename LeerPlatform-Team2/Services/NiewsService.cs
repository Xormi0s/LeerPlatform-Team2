using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class NiewsService : INiewsService
    {
        private GIPContext _ctx;

        public NiewsService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public Nieuwsberichten CreateBericht(Nieuwsberichten bericht)
        {
            _ctx.Add(bericht);
            _ctx.SaveChanges();
            return bericht;
        }

        public void DeleteBericht(Nieuwsberichten bericht)
        {
            _ctx.Remove(bericht);
            _ctx.SaveChanges();
        }

        public Nieuwsberichten EditBericht(Nieuwsberichten bericht)
        {
            _ctx.Update(bericht);
            _ctx.SaveChanges();
            return bericht;
        }

        public IEnumerable<Nieuwsberichten> GetAllNieuws()
        {
            var all = from a in _ctx.Nieuwsberichten
                      select a;
            return all;
        }

        public Nieuwsberichten GetNieuwsbericht(int? id)
        {
            var bericht = from b in _ctx.Nieuwsberichten
                          where b.BerichtenID.Equals(id)
                          select b;
            return bericht.FirstOrDefault();
        }
    }
}
