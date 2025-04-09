using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Models;

namespace WatchList.Controllers
{
    public class WatchController : Controller
    {
        private readonly WatchListContext _context;

        public WatchController(WatchListContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string statusFilter)
        {
            var items = _context.WatchItems.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                items = items.Where(i => i.Status == statusFilter);
            }

            return View(await items.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchItem = await _context.WatchItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchItem == null)
            {
                return NotFound();
            }

            return View(watchItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Type,Status")] WatchItem watchItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watchItem);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchItem = await _context.WatchItems.FindAsync(id);
            if (watchItem == null)
            {
                return NotFound();
            }
            return View(watchItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Type,Status")] WatchItem watchItem)
        {
            if (id != watchItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchItemExists(watchItem.Id))
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
            return View(watchItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchItem = await _context.WatchItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchItem == null)
            {
                return NotFound();
            }

            return View(watchItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchItem = await _context.WatchItems.FindAsync(id);
            _context.WatchItems.Remove(watchItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MarkAsWatched(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchItem = await _context.WatchItems.FindAsync(id);
            if (watchItem == null)
            {
                return NotFound();
            }

            watchItem.Status = "Watched";
            _context.Update(watchItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool WatchItemExists(int id)
        {
            return _context.WatchItems.Any(e => e.Id == id);
        }
    }
}