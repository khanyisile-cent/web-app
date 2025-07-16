using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceAndBooking.Data;
using ResourceAndBooking.Models;

namespace ResourceAndBooking.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var resources = _context.Resources.ToList();
            return View(await _context.Resources.ToListAsync());
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources.FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null) return NotFound();

            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {

            return View(new Resource
            {
                IsAvailable = true
            });
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = resource.Id });
            }
            return View(resource);
        }


        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) return NotFound();

            return View(resource);
        }

        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Capacity,IsAvailable")] Resource resource)
        {
            if (id != resource.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var resource = await _context.Resources.FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null) return NotFound();

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            _ = _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}