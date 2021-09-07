using LeerPlatform_Team2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface INiewsService
    {
        IEnumerable<Nieuwsberichten> GetAllNieuws();
        Nieuwsberichten GetNieuwsbericht(int? id);
        Nieuwsberichten CreateBericht(Nieuwsberichten bericht);
        Nieuwsberichten EditBericht(Nieuwsberichten bericht);
        void DeleteBericht(Nieuwsberichten bericht);

    }
}
