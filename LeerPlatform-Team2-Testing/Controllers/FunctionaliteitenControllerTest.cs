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
using System.Web.Mvc;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class FunctionaliteitenControllerTest
    {
        GIPContext ctx;
        FunctionaliteitenController ctF;

        [TestInitialize]
        public void Init()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            IFunctionaliteitenService svc = new FunctionaliteitenService(ctx);
            ctF = new FunctionaliteitenController(svc);
        }

        [TestMethod]

        public void IndexTest()
        {
            //ACT
            var result = ctF.Index();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]

        public void CreateTest()
        {
            //ACT
            var result = ctF.Create();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]

        public void EditTest()
        {
            //ARRANGE
            TblFunctionaliteiten functionaliteiten= new TblFunctionaliteiten();
            functionaliteiten.FunctionaliteitId = 1;
            functionaliteiten.Beschrijving = "Beamer";
            ctx.Entry(functionaliteiten).State = EntityState.Modified;
            ctF.Edit(1);
            functionaliteiten.FunctionaliteitId = 2;
            functionaliteiten.Beschrijving = "Computers";

            //ACT
            ctF.Create(functionaliteiten);
            string beschrijvingdiff = functionaliteiten.Beschrijving;
            //ASSERT
            Assert.AreEqual(beschrijvingdiff, "Computers");
            Assert.IsNotNull(functionaliteiten);
            Assert.IsInstanceOfType(functionaliteiten, typeof(TblFunctionaliteiten));
            Assert.IsTrue(functionaliteiten is TblFunctionaliteiten);
        }

        [TestMethod]

        public void DeleteTest()
        {
            //ARRANGE
            TblFunctionaliteiten functionaliteiten= new TblFunctionaliteiten();
            functionaliteiten.FunctionaliteitId = 2;

            //ACT
            ctF.Create(functionaliteiten);
            var isCreated = ctx.TblFunctionaliteiten.Count();
            ctF.DeleteConfirmed(2);
            var isDeleted = ctx.TblFunctionaliteiten.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 1);
            Assert.AreEqual(isDeleted, 0);
        }
    }
}
