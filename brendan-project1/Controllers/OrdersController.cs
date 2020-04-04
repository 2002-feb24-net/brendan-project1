using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brendan_project1;
using Microsoft.Extensions.Logging;

namespace brendan_project1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly RestaurantAfrikContext _context;
        private readonly ILogger<OrdersController> logger;
        public OrdersController(RestaurantAfrikContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> SearchLocOrders(string LocName)
        //{
        //    logger.LogInformation($"Finding orders for location: {1}", LocName);
        //    return View("Index", await _context.GetOrds(1, LocName));
        //}
        //public async Task<IActionResult> SearchCustOrders(string firstName, string lastName)
        //{
        //    logger.LogInformation($"Finding orders for customer: {1} {2}", firstName, lastName);
        //    return View("Index", await _context.GetOrders(2, firstName, lastName));
        //}
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var restaurantAfrikContext = _context.Orders.Include(o => o.Customer).Include(o => o.Store);
            return View(await restaurantAfrikContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            ViewData["StoreId"] = new SelectList(_context.StoreLoc, "StoreId", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FoodId,Name,Price,OrderTime,StoreId,CustomerId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.StoreLoc, "StoreId", "Name", orders.StoreId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.StoreLoc, "StoreId", "Name", orders.StoreId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FoodId,Name,Price,OrderTime,StoreId,CustomerId")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.StoreLoc, "StoreId", "Name", orders.StoreId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
