using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services
{
    public class HomeService : IHomeService
    {
        private GIPContext _ctx;

        public HomeService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public List<Nieuwsberichten> GetNieuws()
        {
            return _ctx.Nieuwsberichten.ToList();
        }
    }
}
