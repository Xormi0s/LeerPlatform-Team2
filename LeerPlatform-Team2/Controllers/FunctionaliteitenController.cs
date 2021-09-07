using LeerPlatform_Team2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LeerPlatform_Team2.Controllers
{
    public class FunctionaliteitenController : Controller
    {
        
        private IFunctionaliteitenService _functionaliteitenService;
        private IHomeService svc;

        public object TblFunctionaliteiten { get; set; }

        public FunctionaliteitenController(IFunctionaliteitenService functionaliteitenService)
        {
            _functionaliteitenService = functionaliteitenService;
        }

        public FunctionaliteitenController(IHomeService svc)
        {
            this.svc = svc;
        }

        // GET: Functionaliteiten
        public IActionResult Index()
        {
            return View(_functionaliteitenService.GetAllFuncties());
        }

        // GET: Functionaliteiten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Functionaliteiten/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FunctionaliteitId,Beschrijving")] TblFunctionaliteiten tblFunctionaliteiten)
        {
            if (ModelState.IsValid)
            {
                _functionaliteitenService.CreateFunctionaliteit(tblFunctionaliteiten);
                return RedirectToAction(nameof(Index));
            }
            return View(tblFunctionaliteiten);
        }

            // GET: Functionaliteiten/Edit/5
        public IActionResult Edit(int? id)
        {
            var tblFunctionaliteiten = _functionaliteitenService.GetFunctionaliteit(id);

            if (id == null)
            {
                return NotFound();
            }
            if (tblFunctionaliteiten == null)
            {
                return NotFound();
            }
            return View(tblFunctionaliteiten);
        }

        // POST: Functionaliteiten/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FunctionaliteitId,Beschrijving")] TblFunctionaliteiten tblFunctionaliteiten)
        {
            if (id != tblFunctionaliteiten.FunctionaliteitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     _functionaliteitenService.EditFunctionaliteit(tblFunctionaliteiten);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFunctionaliteitenExists(tblFunctionaliteiten.FunctionaliteitId))
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
            return View(tblFunctionaliteiten);
        }

        // GET: Functionaliteiten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblFunctionaliteiten = _functionaliteitenService.GetFunctionaliteit(id);
            if (tblFunctionaliteiten == null)
            {
                return NotFound();
            }
            return View(tblFunctionaliteiten);
        }

        // POST: Functionaliteiten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblFunctionaliteiten = _functionaliteitenService.GetFunctionaliteit(id);
            _functionaliteitenService.DeleteFunctionaliteit(tblFunctionaliteiten);
            return RedirectToAction(nameof(Index));
        }

        private bool TblFunctionaliteitenExists(int id)
        {
            if (_functionaliteitenService.GetFunctionaliteit(id)!= null)
            {
                return true;
            }
            return false;
        }
    }
}
