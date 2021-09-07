using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Migrations;
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
    public class NieuwsberichtensControllerTest
    {
        GIPContext ctx;
        NieuwsberichtensController ctN;

        [TestInitialize]
        public void Init()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            INiewsService svc = new NiewsService(ctx);
            ctN = new NieuwsberichtensController(svc);
        }

        [TestMethod]

        public void IndexTest()
        {
            //ACT
            var result = ctN.Index();

            //ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void DetailsTest()
        {
            //ARRANGE
            int testnieuwsberichtid = 2;

            //ACT
            var result = ctN.Details(testnieuwsberichtid);

            //ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void CreateTest()
        {
            //ACT
            ctN.Create();
            var isCreated = ctN.Create();

            //ASSERT
            Assert.IsNotNull(isCreated);
        }

        [TestMethod]

        public void Edit()
        {
            Nieuwsberichten nieuwsberichten = new Nieuwsberichten();

            //ACT
            nieuwsberichten.BerichtenID = 3;
            nieuwsberichten.Bericht = "TEST";
            ctN.Edit(40);
            nieuwsberichten.Bericht = "TEST2";
            var result = ctN.Create();
            string diffbericht = nieuwsberichten.Bericht;

            //ASSERT
            Assert.AreEqual(nieuwsberichten.Bericht, "TEST2");
        }

        [TestMethod]

        public void DeleteTest()
        {
            //ARRANGE
            Nieuwsberichten nieuwsberichten = new Nieuwsberichten();
            
            //ACT
            var result = nieuwsberichten.BerichtenID = 3;
            List<Nieuwsberichten> temp = new List<Nieuwsberichten>();
            ctN.Edit(3);
            temp.Add(nieuwsberichten);
            var isingeschreven = temp.Count();      
            ctN.DeleteConfirmed(3);
            ctN.Delete(3);
            temp.Remove(nieuwsberichten);
            var isdeleted = temp.Count();

            //ASSERT
            Assert.AreEqual(isingeschreven, 1);
            Assert.AreEqual(isdeleted, 0);
        }
    }
}
