using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Models;
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
    public class InschrijvingsControllerTest
    {
        GIPContext ctx;
        InschrijvingenController ctL;

        [TestInitialize]
        public void Init()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            IInschrijvingService svc = new InschrijvingService(ctx);
            ctL = new InschrijvingenController(svc);
            Inschrijvingen inschrijving = new Inschrijvingen();
            inschrijving.GebruikerNaam = "Test@gmail.com";
            inschrijving.InschrijvingId = 49;
        }

        [TestMethod]
        public void TestInschrijvingenController_Index()
        {
            //ACT
            var result = ctL.Index();
            //ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void TestInschrijvingenController_DetailsNull()
        {
            //ACT
            var result = ctL.Details(null);
            //ASSERT
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]

        public void TestInschrijvingenController_Details49()
        {
            //ACT
            var result = ctL.Details(49);
            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsNotInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsTrue(result is ViewResult);
        }

        [TestMethod]

        public void TestInschrijvingenController_CreateNotEmpty()
        {
            //ACT
            var result = ctL.Create();

            //ASSERT
            Assert.IsTrue(result is ViewResult);
            Assert.IsNotInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsNotNull(result);
        }
        [TestMethod]

        public void TestInschrijvingenController_CreateEmpty()
        {
            //ARRANGE
            Inschrijvingen inschrijvingen = new Inschrijvingen();
            inschrijvingen.Lescode = string.Empty;

            //ACT
            var result = ctL.Create() ;
            var count = ctx.Inschrijvingen.Count();

            //ASSERT
            Assert.IsTrue(count is 0);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
            
        }

        [TestMethod]

        public void TestInschrijvingenController_Edit()
        {
            //ARRANGE
            Inschrijvingen inschrijvingen = new Inschrijvingen();

            //ACT
            inschrijvingen.Lescode = "GIP";
            var result = ctL.Edit(49);
            ctL.Edit(49, inschrijvingen);
            inschrijvingen.Lescode = "OAA";

            //ASSERT
            Assert.IsNotNull(inschrijvingen.Lescode);
            Assert.IsTrue(inschrijvingen.Lescode is "OAA");
            Assert.IsNotInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]

        public void TestInschrijvingenController_Delete()
        {
            //ARRANGE
            Inschrijvingen inschrijvingen = new Inschrijvingen();
            inschrijvingen.InschrijvingId = 49;
            var result = ctL.Create(inschrijvingen, "OAA");

            //ACT
            var aantal = ctx.Inschrijvingen.Count();
            ctL.DeleteConfirmed(49);
            var aantaldel = ctx.Inschrijvingen.Count();

            //ASSERT
            Assert.IsTrue(aantal is 1);
            Assert.IsNotInstanceOfType(result, typeof(NullReferenceException));
            Assert.IsTrue(aantaldel is 0);
        }
    }
}
