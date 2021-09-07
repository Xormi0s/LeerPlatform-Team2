using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeerPlatform_Team2;
using System.Linq.Dynamic.Core;
using LeerPlatform_Team2.Services;
using LeerPlatform_Team2.Services.Interfaces;

namespace LeerPlatform_Team2.Controllers
{
    public class LessenController : Controller
    {
        private readonly ILessenService _lessenService;

        public LessenController(ILessenService lessenService)
        {
            _lessenService = lessenService;
        }

        // GET: Lessen
        public IActionResult Index()
        {
            return View(_lessenService.GetAllLessen());
        }

        // GET: Lessen/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLessen = _lessenService.GetLes(id);
            if (tblLessen == null)
            {
                return NotFound();
            }

            return View(tblLessen);
        }

        // GET: Lessen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Lescode,Titel,Studiepunten,Reekscode")] TblLessen tblLessen)
        {
            if (ModelState.IsValid)
            {
                if (tblLessen != null && tblLessen.Lescode != null)
                {
                    _lessenService.CreateLes(tblLessen);
                    return RedirectToAction(nameof(Index));
                } 
            }
            return View(tblLessen);
        }

        // GET: Lessen/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLessen = _lessenService.GetLes(id);
            if (tblLessen == null)
            {
                return NotFound();
            }
            return View(tblLessen);
        }

        // POST: Lessen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string LesCode, [Bind("Lescode,Titel,Studiepunten")] TblLessen tblLessen)
        {
            if (LesCode != tblLessen.Lescode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _lessenService.EditLes(tblLessen);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLessenExists(tblLessen.Lescode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblLessen);
        }

        // GET: Lessen/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLessen = _lessenService.GetLes(id);
            if (tblLessen == null)
            {
                return NotFound();
            }

            return View(tblLessen);
        }

        // POST: Lessen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string lesCode)
        {
            var tblLessen = _lessenService.GetLes(lesCode);
            _lessenService.DeleteLes(tblLessen);
            return RedirectToAction(nameof(Index));
        }

        private bool TblLessenExists(string id)
        {
            if(_lessenService.GetLes(id) != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public IActionResult LoadAllLessenSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];

                var lessenData = _lessenService.GetAllLessen(); ;

                int recordsTotal = lessenData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    lessenData = _lessenService.SearchLes(searchValue);
                }
                //sorting
                lessenData = lessenData.OrderBy(sortColumnName + " " + sortDirection);
                //paging
                lessenData = lessenData.Skip(start).Take(length);
                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = lessenData.Count();

                return Json(new { data = lessenData.ToList<TblLessen>(), recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, draw = draw });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
