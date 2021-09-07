﻿using LeerPlatform_Team2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface IInschrijvingPlanService 
    {
        List<TblPlanning> GetPlanList();
        List<Inschrijvingen> GetInschList();
    }
}
