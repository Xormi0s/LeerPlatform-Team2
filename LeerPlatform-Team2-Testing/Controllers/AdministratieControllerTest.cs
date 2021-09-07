using LeerPlatform_Team2;
using LeerPlatform_Team2.Controllers;
using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeerPlatform_Team2_Testing.Controllers
{
    [TestClass]
    public class AdministratieControllerTest
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<TblGebruiker> userManager;

        [TestMethod]
        public void TestIndex()
        {
            //ARRANGE
            var builder = new DbContextOptionsBuilder<GIPContext>();
            builder.UseInMemoryDatabase("GIP");
            GIPContext ctx = new GIPContext(builder.Options);
            AdministratieController ctA = new AdministratieController(ctx, roleManager, userManager);

            //ACT
            IActionResult result = ctA.Index();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsTrue(result is ViewResult);
        }
    }
}
