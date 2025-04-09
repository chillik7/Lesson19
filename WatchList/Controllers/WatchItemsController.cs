using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchList.Data;
using WatchList.Models;

namespace WatchList.Controllers
{
    public class WatchItemsController : Controller
    {
        private readonly WatchListContext _context;

        public WatchItemsController(WatchListContext context)
        {
            _context = context;
        }

        // GET: WatchItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.WatchItems.ToListAsync());
        }

        // GET: WatchItems/Details/5
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

        // GET: WatchItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WatchItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WatchItems/Edit/5
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

        // POST: WatchItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WatchItems/Delete/5
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

        // POST: WatchItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchItem = await _context.WatchItems.FindAsync(id);
            if (watchItem != null)
            {
                _context.WatchItems.Remove(watchItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchItemExists(int id)
        {
            return _context.WatchItems.Any(e => e.Id == id);
        }
    }
}
