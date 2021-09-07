using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class InschrijvingPlanningControllerTest
    {
        GIPContext ctx;
        InschrijvingPlanningController ctP;

        [TestInitialize]
        public void Init()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            IInschrijvingPlanService svc = new InschrijvingPlanService(ctx);
            ctP = new InschrijvingPlanningController(svc);
        }

        [TestMethod]

        public void Indextest()
        {
            //ACT
            var result = ctP.Index();

            //ASSERT
            Assert.IsTrue(result is ViewResult);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
