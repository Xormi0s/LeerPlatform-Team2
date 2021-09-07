using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface ILessenService
    {
        TblLessen GetLes(string id);
        IQueryable<TblLessen> GetAllLessen();
        TblLessen CreateLes(TblLessen tblLessen);
        TblLessen EditLes(TblLessen tblLessen);
        void DeleteLes(TblLessen tblLessen);
        IQueryable<TblLessen> SearchLes(string searchValue);
    }
}
