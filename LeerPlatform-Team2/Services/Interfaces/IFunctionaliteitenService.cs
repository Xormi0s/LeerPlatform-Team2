using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface IFunctionaliteitenService
    {
        TblFunctionaliteiten GetFunctionaliteit(int? id);
        IEnumerable<TblFunctionaliteiten> GetAllFuncties();
        TblFunctionaliteiten CreateFunctionaliteit(TblFunctionaliteiten functie);
        TblFunctionaliteiten EditFunctionaliteit(TblFunctionaliteiten functie);
        void DeleteFunctionaliteit(TblFunctionaliteiten functie);

    }
}
