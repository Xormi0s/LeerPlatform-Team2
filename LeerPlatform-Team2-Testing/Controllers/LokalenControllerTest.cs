using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq.Dynamic;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class LokalenControllerTest
    {
        GIPContext ctx;
        LokalenController ctL;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            ILokalenService svc = new LokalenService(ctx);
            ctL = new LokalenController(svc);
        }

        [TestMethod]
        public void testIndex()
        {
            //ACT
            IActionResult result = (IActionResult)ctL.Index();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.Model is IEnumerable<TblLokalen>);
        }


        [TestMethod]
        public void testDetailsLokaalC001()
        {
            //ARRANGE
            string testNummerLokaal = "c001";

            //ACT
            IActionResult result = (IActionResult)ctL.Details(testNummerLokaal);

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void testDetailsNonExisting()
        {
            //ARRANGE
            string testNummerLokaal = "x987";

            //ACT
            IActionResult result = (IActionResult)ctL.Details(testNummerLokaal);

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is NotFoundResult);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void testCreate()
        {
            //ARRANGE
            TblLokalen testLokaal = new TblLokalen();
            testLokaal.Lokaalnummer = "T123";

            //ACT
            ctL.Create(testLokaal);
            var isCreated = ctx.TblLokalen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 1);
            Assert.IsNotNull(isCreated);
            Assert.IsInstanceOfType(isCreated, typeof(int));
            Assert.IsInstanceOfType(testLokaal, typeof(TblLokalen));
        }

        [TestMethod]
        public void testCreateEmpty()
        {
            //ARRANGE
            if (ctx.TblLokalen.Count() > 0)
            {
                ctL.DeleteConfirmed("T123");
            }
            TblLokalen testLokaal = new TblLokalen();

            //ACT
            ctL.Create(testLokaal);
            var isCreated = ctx.TblLokalen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 0);
            Assert.IsTrue(isCreated is 0);
            Assert.IsInstanceOfType(isCreated, typeof(int));
            Assert.IsNotNull(testLokaal);
            Assert.IsInstanceOfType(testLokaal, typeof(TblLokalen));
        }

        [TestMethod]
        public void testEdit()
        {
            //ARRANGE
            TblLokalen testLokaal = new TblLokalen();
            testLokaal.Lokaalnummer = "c001";
            testLokaal.Capaciteit = 120;
            ctx.Entry(testLokaal).State = EntityState.Modified;
            testLokaal.Lokaalnummer = "c001";
            testLokaal.Capaciteit = 400;
            
            //ACT
            ctL.Create(testLokaal);
            int testCapaciteitVeranderd = (int)testLokaal.Capaciteit;
            //ASSERT
            Assert.AreEqual(testCapaciteitVeranderd, 400);
            Assert.IsNotNull(testLokaal);
            Assert.IsNotNull(testCapaciteitVeranderd);
            Assert.IsInstanceOfType(testLokaal, typeof(TblLokalen));
            
        }


        [TestMethod]
        public void testDelete()
        {
            //ARRANGE
            TblLokalen testLokaal = new TblLokalen();
            testLokaal.Lokaalnummer = "T123";

            //ACT
            ctL.Create(testLokaal);
            var isCreated = ctx.TblLokalen.Count();
            ctL.DeleteConfirmed("T123");
            var isDeleted = ctx.TblLokalen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 1);
            Assert.AreEqual(isDeleted, 0);
        }
    }
}
