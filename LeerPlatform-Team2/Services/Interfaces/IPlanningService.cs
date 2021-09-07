using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface IPlanningService
    {
        IIncludableQueryable<TblPlanning,TblLessenreeks> GetIndex();
        TblPlanning CreatePlan(TblPlanning planning);
        TblPlanning EditPlanning(TblPlanning planning);
        void DeletePlanning(TblPlanning planning);
        IEnumerable<TblLessen> Getlessen();
        IEnumerable<TblLokalen> Getlokaal();
        IEnumerable<TblLessenreeks> GetLessenreeks();
        IQueryable<string> GetLokaalNummer(TblPlanning planning);
        TblPlanning Getplanning(int? id);
        IQueryable<TblPlanning> GetAllPlanning();
        IQueryable<TblPlanning> SearchPlanning(string keyword);


    }
}
