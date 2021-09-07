using LeerPlatform_Team2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Services.Interfaces
{
    public interface IInschrijvingService
    {
        IQueryable<Inschrijvingen> getAllAcc();
        IQueryable<Inschrijvingen> getAllVerwerking();
        IQueryable<StudentenPerPlanning> getIngeschreven(Inschrijvingen inschrijvingen);
        int? getCapaciteit(Inschrijvingen inschrijvingen);
        Task<Inschrijvingen> getInschrijving(int? id);
        IEnumerable<TblLessen> getLessen();
        IQueryable<string> getAllLescodes();
        Inschrijvingen createInsch(Inschrijvingen inschrijvingen);
        Inschrijvingen EditInsch(Inschrijvingen inschrijvingen);
        void DeleteInsch(Inschrijvingen inschrijvingen);
        List<Inschrijvingen> Controle(Inschrijvingen inschrijvingen, string lesCodeList);
        string Lescode(int? id);
        int Aantal(string lescode);
        int Capaciteit(string lescode);
        string User(int inschrijvingId);
        void CreateStudentenPerPlanning(StudentenPerPlanning studenten);
        IQueryable<Inschrijvingen> SearchVerwerking(string keyword);
        IQueryable<Inschrijvingen> SearchAccepted(string keyword);
    }
}
