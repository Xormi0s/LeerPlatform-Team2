using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LeerPlatform_Team2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SQLitePCL;

namespace LeerPlatform_Team2.Controllers
{
    [Authorize(Roles =("Admin"))]
    public class AdministratieController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<TblGebruiker> userManager;
        private GIPContext ctx;

        public AdministratieController(GIPContext ctx, RoleManager<IdentityRole> roleManager,
                                        UserManager<TblGebruiker> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.ctx = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Roles beheer
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        //Roles beheer
        [HttpGet]
        public async Task<IActionResult> EditRoleAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        public IActionResult EditRole(string inschrijvingsid)
        {
            throw new NotImplementedException();
        }

        //Roles beheer
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
          
            role.Name = model.RoleName;

            // Update the Role using UpdateAsync
            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {                 
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
               ModelState.AddModelError("", error.Description);
            }

            return View(model);           
        }

        public IActionResult EditRole(IQueryable<IdentityRole> roles)
        {
            throw new NotImplementedException();
        }

        //Roles beheer
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
              var userRoleViewModel = new UserRoleViewModel
              {
                  UserId = user.Id,
                  UserName = user.UserName
              };
              if (await userManager.IsInRoleAsync(user, role.Name))
              {
                 userRoleViewModel.IsSelected = true;
              }
              else
              {
                 userRoleViewModel.IsSelected = false;
              }             
              model.Add(userRoleViewModel);
            }
              return View(model);     
        }

        //Roles beheer
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
             
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    if ((model[i].UserName == "adw-admin@ucll.be"||model[i].UserName =="admin@ucll.be") && roleId == "53ed032a-50d7-4e20-ba26-9b9a01595e60")
                    {
                        continue;
                    }
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
    
        //User beheer
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        //User beheer
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }

            foreach (var error in result.Errors)
            {
               ModelState.AddModelError("", error.Description);
            }
            return View("ListUsers");            
        }
    }
}