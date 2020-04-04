using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brendan_project1;

namespace brendan_project1.Controllers
{
    public class StoreLocsController : Controller
    {
        private readonly RestaurantAfrikContext _context;

        public StoreLocsController(RestaurantAfrikContext context)
        {
            _context = context;
        }

        // GET: StoreLocs
        public async Task<IActionResult> Index()
        {
            return View(await _context.StoreLoc.ToListAsync());
        }

        // GET: StoreLocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLoc = await _context.StoreLoc
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeLoc == null)
            {
                return NotFound();
            }

            return View(storeLoc);
        }

        // GET: StoreLocs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreLocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,Name,Phone,Email,Street,City,State,ZipCode")] StoreLoc storeLoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeLoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeLoc);
        }

        // GET: StoreLocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLoc = await _context.StoreLoc.FindAsync(id);
            if (storeLoc == null)
            {
                return NotFound();
            }
            return View(storeLoc);
        }

        // POST: StoreLocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,Name,Phone,Email,Street,City,State,ZipCode")] StoreLoc storeLoc)
        {
            if (id != storeLoc.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeLoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreLocExists(storeLoc.StoreId))
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
            return View(storeLoc);
        }

        // GET: StoreLocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeLoc = await _context.StoreLoc
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (storeLoc == null)
            {
                return NotFound();
            }

            return View(storeLoc);
        }

        // POST: StoreLocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeLoc = await _context.StoreLoc.FindAsync(id);
            _context.StoreLoc.Remove(storeLoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreLocExists(int id)
        {
            return _context.StoreLoc.Any(e => e.StoreId == id);
        }
    }
}
