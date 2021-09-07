using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface ILokalenService
    {
        IIncludableQueryable<TblLokalen, TblFunctionaliteiten> GetIndex();
        TblLokalen CreateLokaal(TblLokalen lokaal);
        TblLokalen EditLokaal(TblLokalen lokaal);
        void DeleteLokaal(TblLokalen lokaal);
        TblLokalen GetLokaal(string lokaalNummer);
        IEnumerable<TblFunctionaliteiten> GetFunct();
        IQueryable<string> GetLokaalNummers();
        List<TblLokalen> GetAll();
        IQueryable<TblLokalen> GetAllLokalen();
        IQueryable<TblLokalen> SearchLokalen(string keyword);
    }
}
