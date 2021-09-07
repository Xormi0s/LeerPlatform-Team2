using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeerPlatform_Team2;
using LeerPlatform_Team2.Models;
using LeerPlatform_Team2.Services.Interfaces;

namespace LeerPlatform_Team2.Controllers
{
    public class NieuwsberichtensController : Controller
    {
        private INiewsService _nieuwsService;

        public NieuwsberichtensController(INiewsService niewsService)
        {
            _nieuwsService = niewsService;
        }

        // GET: Nieuwsberichtens
        public async Task<IActionResult> Index()
        {
            return View(_nieuwsService.GetAllNieuws());
        }

        // GET: Nieuwsberichtens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var nieuwsberichten = _nieuwsService.GetNieuwsbericht(id);
            if (nieuwsberichten == null)
            {
                return NotFound();
            }
            return View(nieuwsberichten);
        }

        // GET: Nieuwsberichtens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nieuwsberichtens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BerichtenID,Datum,Titel,Bericht")] Nieuwsberichten nieuwsberichten)
        {
            if (ModelState.IsValid)
            {
                _nieuwsService.CreateBericht(nieuwsberichten);
                return RedirectToAction(nameof(Index));
            }
            return View(nieuwsberichten);
        }

        // GET: Nieuwsberichtens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuwsberichten = _nieuwsService.GetNieuwsbericht(id);
            if (nieuwsberichten == null)
            {
                return NotFound();
            }
            return View(nieuwsberichten);
        }

        // POST: Nieuwsberichtens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BerichtenID,Datum,Titel,Bericht")] Nieuwsberichten nieuwsberichten)
        {
            if (id != nieuwsberichten.BerichtenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _nieuwsService.EditBericht(nieuwsberichten);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NieuwsberichtenExists(nieuwsberichten.BerichtenID))
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
            return View(nieuwsberichten);
        }

        // GET: Nieuwsberichtens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuwsberichten = _nieuwsService.GetNieuwsbericht(id);
            if (nieuwsberichten == null)
            {
                return NotFound();
            }

            return View(nieuwsberichten);
        }

        // POST: Nieuwsberichtens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuwsberichten = _nieuwsService.GetNieuwsbericht(id);
            _nieuwsService.DeleteBericht(nieuwsberichten);
            return RedirectToAction(nameof(Index));
        }

        private bool NieuwsberichtenExists(int id)
        {
            if (_nieuwsService.GetNieuwsbericht(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
