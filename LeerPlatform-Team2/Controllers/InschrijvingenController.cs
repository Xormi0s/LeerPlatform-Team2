using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeerPlatform_Team2;
using LeerPlatform_Team2.Models;
using Microsoft.AspNetCore.Identity;
using LeerPlatform_Team2.Migrations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Claims;
using LeerPlatform_Team2.Services.Interfaces;

namespace LeerPlatform_Team2.Controllers
{
    public class InschrijvingenController : Controller
    {
        private readonly IInschrijvingService _inschrijvingService;
        public InschrijvingenController(IInschrijvingService inschrijvingService)
        {
            _inschrijvingService = inschrijvingService;
        }

        // GET: Inschrijvingen
        public IActionResult Index()
        {
            return View();
        }

        // GET: Inschrijvingen/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var inschrijvingen = _inschrijvingService.getInschrijving(id);
            if (inschrijvingen == null)
            {
                return NotFound();
            }

            return View(inschrijvingen);
        }

        // GET: Inschrijvingen/Create
        public IActionResult Create()
        {
            ViewBag.les = new SelectList(_inschrijvingService.getLessen(), "Lescode", "Titel"); ;
            return View();
        }

        // POST: Inschrijvingen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InschrijvingId,Status,GebruikerNaam,Lescode,GebruikerNavigationId")] Inschrijvingen inschrijvingen, string LescodeList)
        {
            ViewBag.les = new SelectList(_inschrijvingService.getLessen(), "Lescode", "Titel");

            inschrijvingen.Status = Status.verwerking;
            // Tot hier is het geimplementeerd... nog ver der afwerken...
            var controleQ = _inschrijvingService.Controle(inschrijvingen, LescodeList);

            List<Inschrijvingen> temp = new List<Inschrijvingen>();
            temp.AddRange(controleQ);
            int count = 0;

            if (temp.Count >= 1)
            {
                if (temp[0].Lescode != null)
                {
                    count = controleQ.Count();
                }
            }

            if (count >= 1)
            {
                ModelState.AddModelError(nameof(inschrijvingen.Lescode), "U bent al reeds ingeschreven voor dit vak !");
            }

            if (ModelState.IsValid)
            {
                inschrijvingen.Lescode = LescodeList;
                _inschrijvingService.createInsch(inschrijvingen);
                return RedirectToAction("Index", "InschrijvingPlanning");
            }
            return View();
        }

        // GET: Inschrijvingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string lescode = _inschrijvingService.Lescode(id);
            int aantal = _inschrijvingService.Aantal(lescode);

            if (aantal >= 1)
            {
                ViewBag.Aantal = aantal;
            }
            else
            {
                ViewBag.Aantal = 0;
            }

            ViewBag.Capaciteit = _inschrijvingService.Capaciteit(lescode);

            var inschrijvingen = await _inschrijvingService.getInschrijving(id);
            if (inschrijvingen == null)
            {
                return NotFound();
            }
            return View(inschrijvingen);
        }

        // POST: Inschrijvingen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int inschrijvingId, [Bind("InschrijvingId,Status,GebruikerNaam,Lescode,GebruikerNavigationId")] Inschrijvingen inschrijvingen)
        {
            inschrijvingen.GebruikerNavigationId = _inschrijvingService.User(inschrijvingId);

            if (inschrijvingId != inschrijvingen.InschrijvingId)
            {
                return NotFound();
            }

            string lescode = _inschrijvingService.Lescode(inschrijvingId);
            int aantal = _inschrijvingService.Aantal(lescode);
            int capaciteit = _inschrijvingService.Capaciteit(lescode);

            if (aantal < capaciteit && inschrijvingen.Status == Status.geaccepteerd)
            {
                StudentenPerPlanning temp = new StudentenPerPlanning();
                temp.Gebruikersnaam = inschrijvingen.GebruikerNaam;
                temp.Lescode = inschrijvingen.Lescode;
                _inschrijvingService.CreateStudentenPerPlanning(temp);
            }
            if (aantal >= capaciteit)
            {
                ModelState.AddModelError(nameof(inschrijvingen.Lescode), "De capaciteit voor dit vak is al bereikt ! Er kunnen geen studenten meer ingeschreven worden.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _inschrijvingService.EditInsch(inschrijvingen);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InschrijvingenExists(inschrijvingen.InschrijvingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Student"))
                {
                    return RedirectToAction("Index", "InschrijvingPlanning");
                }
                return RedirectToAction("index", "Inschrijvingen");

            }
            return View(inschrijvingen);
        }

        // GET: Inschrijvingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inschrijvingen = await _inschrijvingService.getInschrijving(id);
            if (inschrijvingen == null)
            {
                return NotFound();
            }

            return View(inschrijvingen);
        }

        // POST: Inschrijvingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int inschrijvingId)
        {
            var inschrijvingen = await _inschrijvingService.getInschrijving(inschrijvingId);
            _inschrijvingService.DeleteInsch(inschrijvingen);
            if (User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "InschrijvingPlanning");
            }
            return RedirectToAction("Index", "Inschrijvingen");
        }

        private bool InschrijvingenExists(int id)
        {
            if (_inschrijvingService.getInschrijving(id) != null)
            {
                return true;
            }
            return false;
        }

        public IActionResult Ingeschreven(string Lescode)
        {
            var output = _inschrijvingService.getAllAcc();
            var lescodeQ = _inschrijvingService.getAllLescodes();

            ViewBag.LescodeList = new SelectList(lescodeQ);

            if (!string.IsNullOrEmpty(Lescode))
            {
                output = output.Where(q => q.Lescode.Contains(Lescode));
            }

            var test = output;

            return View(output);
        }

        [HttpPost]
        public IActionResult LoadAllInschrijvingenSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];

                var inschrijvingData = _inschrijvingService.getAllVerwerking();

                int recordsTotal = inschrijvingData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    inschrijvingData = _inschrijvingService.SearchVerwerking(searchValue);
                }

                //sorting
                inschrijvingData = inschrijvingData.OrderBy(sortColumnName + " " + sortDirection);

                //paging
                inschrijvingData = inschrijvingData.Skip(start).Take(length);

                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = inschrijvingData.Count();

                return Json(new { data = inschrijvingData.ToList<Inschrijvingen>(), recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, draw = draw });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult LoadAllIngeschrevenSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];

                var inschrijvingData = _inschrijvingService.getAllAcc();
                int recordsTotal = inschrijvingData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    inschrijvingData = _inschrijvingService.SearchAccepted(searchValue);
                }

                //sorting
                inschrijvingData = inschrijvingData.OrderBy(sortColumnName + " " + sortDirection);

                //paging
                inschrijvingData = inschrijvingData.Skip(start).Take(length);

                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = inschrijvingData.Count();

                return Json(new { data = inschrijvingData.ToList<Inschrijvingen>(), recordsTotal = recordsTotal, recordsFiltered = recordsFiltered, draw = draw });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
