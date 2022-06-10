using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movieDB.Models;

namespace MvcMovie.Controllers
{
    public class WantseeController : Controller
    {
        private readonly MvcWantseeContext _context;

        public WantseeController(MvcWantseeContext context)
        {
            _context = context;
        }

        // GET: Wantsee
        public async Task<IActionResult> Index()
        {
              return View(await _context.wantsee.ToListAsync());
        }

        // GET: Wantsee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.wantsee == null)
            {
                return NotFound();
            }

            var wantsee = await _context.wantsee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wantsee == null)
            {
                return NotFound();
            }

            return View(wantsee);
        }

        // GET: Wantsee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wantsee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,MovieID")] wantsee wantsee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wantsee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wantsee);
        }

        // GET: Wantsee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.wantsee == null)
            {
                return NotFound();
            }

            var wantsee = await _context.wantsee.FindAsync(id);
            if (wantsee == null)
            {
                return NotFound();
            }
            return View(wantsee);
        }

        // POST: Wantsee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,MovieID")] wantsee wantsee)
        {
            if (id != wantsee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wantsee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!wantseeExists(wantsee.ID))
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
            return View(wantsee);
        }

        // GET: Wantsee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.wantsee == null)
            {
                return NotFound();
            }

            var wantsee = await _context.wantsee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wantsee == null)
            {
                return NotFound();
            }

            return View(wantsee);
        }

        // POST: Wantsee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.wantsee == null)
            {
                return Problem("Entity set 'MvcWantseeContext.wantsee'  is null.");
            }
            var wantsee = await _context.wantsee.FindAsync(id);
            if (wantsee != null)
            {
                _context.wantsee.Remove(wantsee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool wantseeExists(int id)
        {
          return _context.wantsee.Any(e => e.ID == id);
        }
    }
}
