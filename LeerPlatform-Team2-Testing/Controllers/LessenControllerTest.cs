using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class LessenControllerTest
    {
        GIPContext ctx;
        LessenController ctL;

        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            ILessenService svc = new LessenService(ctx);
            ctL = new LessenController(svc);
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
            Assert.IsTrue(viewResult.Model is IEnumerable<TblLessen>);
        }

        [TestMethod]
        public void testDetailsLessenOOA()
        {
            //ARRANGE
            string testNummerLessen = "SEC";

            //ACT
            IActionResult result = (IActionResult)ctL.Details(testNummerLessen);

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public void testDetailsNonExisting()
        {
            //ARRANGE
            string testNummerLessen = "x987";

            //ACT
            IActionResult result = (IActionResult)ctL.Details(testNummerLessen);

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is NotFoundResult);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void testCreate()
        {
            //ARRANGE
            TblLessen testLes = new TblLessen();
            testLes.Lescode = "SEC";

            //ACT
            ctL.Create(testLes);
            var isCreated = ctx.TblLessen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 1);
            Assert.IsNotNull(isCreated);
        }

        [TestMethod]
        public void testCreateEmpty()
        {
            //ARRANGE
            if(ctx.TblLessen.Count() > 0)
            {
                ctL.DeleteConfirmed("SEC");
            }
            TblLessen testLes = new TblLessen();

            //ACT
            ctL.Create(testLes);
            var isCreated = ctx.TblLessen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 0);
            Assert.IsTrue(isCreated is 0);
            Assert.IsInstanceOfType(isCreated, typeof(int));
        }

        [TestMethod]
        public void testEdit()
        {
            //ARRANGE
            TblLessen testles = new TblLessen();

            //ACT
            testles.Lescode = "GIP";
            var result = ctL.Edit("GIP");
            testles.Lescode = "OAA";

            //ASSERT
            Assert.IsNotNull(testles.Lescode);
            Assert.IsTrue(testles.Lescode is "OAA");
            Assert.IsNotInstanceOfType(testles, typeof(NotFoundResult));
        }


        [TestMethod]
        public void testDelete()
        {
            //ARRANGE
            TblLessen testLes = new TblLessen();
            testLes.Lescode = "SEC";

            //ACT
            ctL.Create(testLes);
            var isCreated = ctx.TblLessen.Count();
            ctL.DeleteConfirmed("SEC");
            var isDeleted = ctx.TblLessen.Count();

            //ASSERT
            Assert.AreEqual(isCreated, 1);
            Assert.AreEqual(isDeleted, 0);
        }
    }
}
