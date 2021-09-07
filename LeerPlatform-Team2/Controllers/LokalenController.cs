using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeerPlatform_Team2;
using System.Linq.Dynamic.Core;
using LeerPlatform_Team2.Services.Interfaces;

namespace LeerPlatform_Team2.Controllers
{
    public class LokalenController : Controller
    {

        private ILokalenService _lokalenService;

        public LokalenController(ILokalenService lokalenService)
        {
            _lokalenService = lokalenService;
        }

        // GET: Lokalen
        public IActionResult Index()
        {
            return View(_lokalenService.GetIndex());
        }

        // GET: Lokalen/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gIPContext = _lokalenService.GetIndex();

            var tblLokalen = _lokalenService.GetLokaal(id);
            if (tblLokalen == null)
            {
                return NotFound();
            }

            return View(tblLokalen);
        }

        // GET: Lokalen/Create
        public IActionResult Create()
        {
            ViewData["FunctionaliteitenId"] = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "FunctionaliteitId");
            ViewBag.functie = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "Beschrijving");
            return View();
        }

        // POST: Lokalen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Lokaalnummer,Locatie,Capaciteit,FunctionaliteitenId")] TblLokalen tblLokalen)
        {
          
                if (ModelState.IsValid)
                {
                    if(tblLokalen != null && tblLokalen.Lokaalnummer != null)
                    {
                    _lokalenService.CreateLokaal(tblLokalen);
                        return RedirectToAction(nameof(Index));
                    }
                }            
                ViewBag.functie = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "Beschrijving");
                return View(tblLokalen); 
        }

        // GET: Lokalen/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLokalen = _lokalenService.GetLokaal(id);
            if (tblLokalen == null)
            {
                return NotFound();
            }
            ViewData["FunctionaliteitenId"] = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "FunctionaliteitId", tblLokalen.FunctionaliteitenId);
            ViewBag.functie = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "Beschrijving");
            return View(tblLokalen);
        }

        // POST: Lokalen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Lokaalnummer, [Bind("Lokaalnummer,Locatie,Capaciteit,FunctionaliteitenId")] TblLokalen tblLokalen)
        {
            if (Lokaalnummer != tblLokalen.Lokaalnummer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _lokalenService.EditLokaal(tblLokalen);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLokalenExists(tblLokalen.Lokaalnummer))
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
            ViewBag.functie = new SelectList(_lokalenService.GetFunct(), "FunctionaliteitId", "Beschrijving");
            return View(tblLokalen);
        }

        // GET: Lokalen/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLokalen = _lokalenService.GetLokaal(id);
            if (tblLokalen == null)
            {
                return NotFound();
            }

            return View(tblLokalen);
        }

        // POST: Lokalen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string Lokaalnummer)
        {
            var tblLokalen = _lokalenService.GetLokaal(Lokaalnummer);
            _lokalenService.DeleteLokaal(tblLokalen);
            return RedirectToAction(nameof(Index));
        }

        private bool TblLokalenExists(string id)
        {
            if (_lokalenService.GetLokaal(id)!=null)
            {
                return true;
            }
            return false;
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult Validatelokaal(string Lokaalnummer)
        {
            var lokaal = _lokalenService.GetLokaalNummers();
            if (lokaal.Contains(Lokaalnummer))
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }

        public IActionResult LoadAllLokalen()
        {
            try {
                var lokalenData = _lokalenService.GetAll();
                return Json(new{  data = lokalenData });
            }
            catch(Exception){
                throw;
            }
        }

        [HttpPost]
        public IActionResult LoadAllLokalenSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];

                var lokalenData = _lokalenService.GetAllLokalen();
                int recordsTotal = lokalenData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    lokalenData = _lokalenService.SearchLokalen(searchValue);
                }

                //sorting
                lokalenData = lokalenData.OrderBy(sortColumnName + " " + sortDirection);

                //paging
                lokalenData = lokalenData.Skip(start).Take(length);

                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = lokalenData.Count();

                return Json(new { data = lokalenData.ToList<TblLokalen>(), recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, draw = draw });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
