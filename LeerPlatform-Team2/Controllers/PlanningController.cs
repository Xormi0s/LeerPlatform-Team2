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
    public class PlanningController : Controller
    {
        private IPlanningService _planningService;

        public PlanningController(IPlanningService planningService)
        {
            _planningService = planningService;
        }

        // GET: Planning
        public IActionResult Index()
        { 
            return View(_planningService.GetIndex());         
        }

        // GET: Planning/Create
        public IActionResult Create()
        {
            ViewData["Lescode"] = new SelectList(_planningService.Getlessen(), "Lescode", "Lescode");
            ViewData["Lokaalnummer"] = new SelectList(_planningService.Getlokaal(), "Lokaalnummer", "Lokaalnummer");
            ViewData["Reekscode"] = new SelectList(_planningService.GetLessenreeks(), "Reekscode", "Reekscode");
            return View();
        }

        // POST: Planning/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanningId,Lokaalnummer,Lescode,Reekscode,StartTijdstip,EindTijdstip,ExtraInfo")] TblPlanning tblPlanning)
        {
            ViewData["Lescode"] = new SelectList(_planningService.Getlessen(), "Lescode", "Lescode", tblPlanning.Lescode);
            ViewData["Lokaalnummer"] = new SelectList(_planningService.Getlokaal(), "Lokaalnummer", "Lokaalnummer", tblPlanning.Lokaalnummer);
            ViewData["Reekscode"] = new SelectList(_planningService.GetLessenreeks(), "Reekscode", "Reekscode", tblPlanning.Reekscode);

            var queryD = _planningService.GetLokaalNummer(tblPlanning);
            List<string> outputQ = new List<string>();
            outputQ.AddRange(queryD);

            if(outputQ.Count >= 1)
            {
                if (outputQ.Contains(tblPlanning.Lokaalnummer.ToString()))
                {
                    ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een ander lokaal te kiezen. Deze is al bezet !");
                }
            }

            if(tblPlanning.StartTijdstip < DateTime.Now)
            {
                ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een geldig start tijdstip te kiezen !");
            }

            if (tblPlanning.EindTijdstip < tblPlanning.StartTijdstip)
            {
                ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een geldig eind tijdstip te kiezen !");
            }

            if (ModelState.IsValid)
            {
                _planningService.CreatePlan(tblPlanning);
                return RedirectToAction(nameof(Index));
            }
            return View(tblPlanning);
        }

        // GET: Planning/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPlanning = _planningService.Getplanning(id);
            if (tblPlanning == null)
            {
                return NotFound();
            }
            ViewData["Lescode"] = new SelectList(_planningService.Getlessen(), "Lescode", "Lescode", tblPlanning.Lescode);
            ViewData["Lokaalnummer"] = new SelectList(_planningService.Getlokaal(), "Lokaalnummer", "Lokaalnummer", tblPlanning.Lokaalnummer);
            ViewData["Reekscode"] = new SelectList(_planningService.GetLessenreeks(), "Reekscode", "Reekscode", tblPlanning.Reekscode);
            return View(tblPlanning);
        }

        // POST: Planning/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int PlanningId, [Bind("PlanningId,Lokaalnummer,Lescode,Reekscode,StartTijdstip,EindTijdstip,ExtraInfo")] TblPlanning tblPlanning)
        {
            if (PlanningId != tblPlanning.PlanningId)
            {
                return NotFound();
            }

            var queryD = _planningService.GetLokaalNummer(tblPlanning);
            List<string> outputQ = new List<string>();
            outputQ.AddRange(queryD);

            if (outputQ.Count >= 1)
            {
                if (outputQ.Contains(tblPlanning.Lokaalnummer.ToString()))
                {
                    ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een ander lokaal te kiezen. Deze is al bezet !");
                }
            }

            if (tblPlanning.StartTijdstip < DateTime.Now)
            {
                ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een geldig start tijdstip te kiezen !");
            }

            if (tblPlanning.EindTijdstip < tblPlanning.StartTijdstip)
            {
                ModelState.AddModelError(nameof(tblPlanning.ExtraInfo), "Gelieve een geldig eind tijdstip te kiezen !");
            }

            if (ModelState.IsValid)
            {
                _planningService.EditPlanning(tblPlanning);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Lescode"] = new SelectList(_planningService.Getlessen(), "Lescode", "Lescode", tblPlanning.Lescode);
            ViewData["Lokaalnummer"] = new SelectList(_planningService.Getlokaal(), "Lokaalnummer", "Lokaalnummer", tblPlanning.Lokaalnummer);
            ViewData["Reekscode"] = new SelectList(_planningService.GetLessenreeks(), "Reekscode", "Reekscode", tblPlanning.Reekscode);
            return View(tblPlanning);
        }

        // GET: Planning/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPlanning = _planningService.Getplanning(id);
            if (tblPlanning == null)
            {
                return NotFound();
            }

            return View(tblPlanning);
        }

        // POST: Planning/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int planningId)
        {
            var tblPlanning = _planningService.Getplanning(planningId);
            _planningService.DeletePlanning(tblPlanning);
            return RedirectToAction(nameof(Index));
        }

        private bool TblPlanningExists(int id)
        {
            if(_planningService.Getplanning(id) != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public IActionResult LoadAllPlanningSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];

                var planningData = _planningService.GetAllPlanning();
                int recordsTotal = planningData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    planningData = _planningService.SearchPlanning(searchValue);
                }
                //sorting
                planningData = planningData.OrderBy(sortColumnName + " " + sortDirection);

                //paging
                planningData = planningData.Skip(start).Take(length);

                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = planningData.Count();

                return Json(new { data = planningData.ToList<TblPlanning>(), recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, draw = draw });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
