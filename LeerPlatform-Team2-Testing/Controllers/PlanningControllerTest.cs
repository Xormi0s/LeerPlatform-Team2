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

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class PlanningControllerTest
    {
        GIPContext ctx;
        PlanningController ctP;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            IPlanningService svc = new PlanningService(ctx);
            ctP = new PlanningController(svc);
        }

        [TestMethod]
        public void testIndex()
        {
            //ACT
            IActionResult result = (IActionResult)ctP.Index();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.Model is IEnumerable<TblPlanning>);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]

        public void testCreate()
        {
            //ACT
            IActionResult result = (IActionResult)ctP.Create();
            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]

        public void testEdit()
        {
            //ARRANGE
            TblPlanning testplanning = new TblPlanning();
            testplanning.PlanningId = 1;
            testplanning.Lokaalnummer = "c001";
            ctx.Entry(testplanning).State = EntityState.Modified;
            testplanning.PlanningId = 2;
            testplanning.Lokaalnummer = "c312";

            //ACT
            ctP.Create(testplanning);
            int testidchange = (int)testplanning.PlanningId;
            //ASSERT
            Assert.AreEqual(testidchange, 2);
            Assert.IsInstanceOfType(testplanning, typeof(TblPlanning));
            Assert.IsInstanceOfType(testplanning.Lokaalnummer, typeof(string));
            Assert.IsInstanceOfType(testplanning.PlanningId, typeof(int));
        }

        [TestMethod]

        public void testDelete()
        {
            //ARRANGE
            TblPlanning testplanning = new TblPlanning();
            testplanning.PlanningId = 4;

            //ACT
            ctP.Create(testplanning);
            var isCreated = ctx.TblPlanning.Count();
            ctP.DeleteConfirmed(4);
            var isDeleted = ctx.TblPlanning.Count();

            //ASSERT
            Assert.AreEqual(isDeleted, 0);
            Assert.IsInstanceOfType(testplanning, typeof(TblPlanning));
            Assert.IsInstanceOfType(testplanning.PlanningId, typeof(int));
        }

       
    }
}
