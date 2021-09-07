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
using System.Text;
using System.Web.Mvc;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        GIPContext ctx;
        HomeController ctH;

        [TestInitialize]
        public void Init()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            ctx = new GIPContext(builder.Options);
            IHomeService svc = new HomeService(ctx);
            ctH = new HomeController(svc);
        }

        [TestMethod]

        public void IndexTest()
        {
            //ACT
            var result = ctH.Index();

            //ARRANGE
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]

        public void PrivacyTest()
        {
            //ACT
            var result = ctH.Privacy();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is IActionResult);
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }
    }
}
