using LeerPlatform_Team2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LeerPlatform_Team2.Services
{
    public class RegisterService : IRegisterService
    {
        private GIPContext _ctx;
        public RegisterService(GIPContext ctx)
        {
            _ctx = ctx;
        }

        public string getNummer(string uNummer)
        {
            var nummer = from n in _ctx.TblGebruiker
                         where n.UcllNummer == uNummer
                         select n.ToString();
            return nummer.FirstOrDefault();
        }
    }
}
