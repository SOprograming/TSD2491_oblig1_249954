using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using TSD2491_OBLIG1_249954.Data;
using TSD2491_OBLIG1_249954.Models;

namespace TSD2491_OBLIG1_249954.Controllers
{
    public class BrukerController : Controller
    {
        private readonly BrukerContext _context;

        static Bruker tmpBruker = new Bruker();
        

        // GET: BrukerController
        public BrukerController(BrukerContext context)
        {
            _context = context;
        }
        
        public IActionResult Spill()
        {
            // Sorterer de topp 5 brukerene
            var topp5 = _context.Bruker
                .OrderByDescending(b => b.AntallSpill)
                .Take(5)
                .ToList();

            var valgtBruker = _context.Bruker.FirstOrDefault(b => b.Navn == tmpBruker.Navn);
            if (valgtBruker != null)
            {
                ViewBag.Message = valgtBruker.Navn;
                ViewData["bruker"] = valgtBruker.Navn;
            }


            return View(topp5);
        }

        [HttpPost]
        public IActionResult OppdaterAntallSpill(string navn){
            if(string.IsNullOrEmpty(navn)){
                return View("IkkeFunnet");
            }
            
            Bruker bruker = tmpBruker;
            var valgtBruker = _context.Bruker.FirstOrDefault(b => b.Navn == bruker.Navn);

            if (valgtBruker != null)
            {
                ViewData["bruker"] = navn;   
                valgtBruker.AntallSpill++;
                _context.SaveChanges();

                return View("OppdaterBrukerModell");
               
            }

            return View("IkkeFunnet");
        }

        [HttpPost]
        public IActionResult RegistrereBruker(string navn){
            if(string.IsNullOrEmpty(navn)){
                return View("IkkeFunnet");
            }

            tmpBruker = new Bruker{Navn = navn.Trim()};
            var valgtBruker = _context.Bruker.FirstOrDefault(b => b.Navn == tmpBruker.Navn);

            if (valgtBruker != null)
            {
                ViewData["bruker"] = navn;    
                return View("BrukerRegistrert");
            
                
            }

            return View("IkkeFunnet");
        }
      

        // GET: Bruker
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bruker.ToListAsync());
        }

        // GET: Bruker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bruker == null)
            {
                return NotFound();
            }

            return View(bruker);
        }

        // GET: Bruker/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bruker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Navn,AntallSpill,KontaktInfo")] Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bruker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bruker);
        }

        // GET: Bruker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker.FindAsync(id);
            if (bruker == null)
            {
                return NotFound();
            }
            return View(bruker);
        }

        // POST: Bruker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Navn,AntallSpill,KontaktInfo")] Bruker bruker)
        {
            if (id != bruker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bruker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrukerExists(bruker.ID))
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
            return View(bruker);
        }

        // GET: Bruker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bruker = await _context.Bruker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bruker == null)
            {
                return NotFound();
            }

            return View(bruker);
        }

        // POST: Bruker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bruker = await _context.Bruker.FindAsync(id);
            if (bruker != null)
            {
                _context.Bruker.Remove(bruker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrukerExists(int id)
        {
            return _context.Bruker.Any(e => e.ID == id);
        }
    }
}
