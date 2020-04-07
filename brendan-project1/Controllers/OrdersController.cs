using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using brendan_project1;
using Microsoft.Extensions.Logging;
using brendan_project1.Domain.Interfaces;
using brendan_project1.Models;
using System.Text.Json;

namespace brendan_project1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersRepo _ordersRepo;
        private readonly RestaurantAfrikContext _context;
        private readonly ILogger<OrdersController> logger;
        public OrdersController(RestaurantAfrikContext context)
        {
            _context = context;
        }
        public IActionResult Searchorders(string search)
        {
            /*       return View("Index", _ordersRepo.SearchOrders(firstName, lastName));*/

            var orderSearch = from c in _context.Orders
                              where c.Customer.FirstName == search || c.Customer.LastName == search
                              select c;

            if (search != null)
            {
                orderSearch = orderSearch.Where(s => s.Customer.FirstName.ToUpper().Contains(search) || s.Customer.LastName.ToUpper().Contains(search));
            }
            return View(orderSearch);
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
            ViewData["FoodId"] = new SelectList(_context.Foods, "FoodId", "Name");
            var ovm = new OrderViewModel
            {
                Inventory = _context.Foods.ToList()
            };
            return View(ovm);
        }

        public IActionResult Submit()
        {
            if(TempData["Cart"]!=null)
            {

                //create order then order lines, add to context
                var order = new Orders
                {
                    CustomerId = Convert.ToInt32(TempData["CustID"]),
                    StoreId = Convert.ToInt32(TempData["LocID"]),
                    Price = 0,
                    OrderTime = DateTime.Now,
                    Name = "",
                    FoodId = 0
                };
                var cart = JsonSerializer.Deserialize<List<List<int>>>(TempData["Cart"].ToString());
                _context.Orders.Add(order);
                _context.SaveChanges();
                _context.Entry(order).Reload();
                int orderID = order.OrderId;
                foreach (var orders in cart)
                {
                    var orderline = new Orderline
                    {
                        Orderid = orderID,
                        Foodid = orders[0],
                        Qty = orders[1]
                    };
                    _context.Orderline.Add(orderline);
                    _context.SaveChanges();
                }
                
                
                
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddOrderItem(int productId, int quantity)
        {
            List<List<int>> productIds = new List<List<int>>(); //each value in product is list of int, each list is length 2, 1st value is pid, 2nd is quantity
            if (TempData["Cart"] != null) //cart has somehting in it, we can add productid to it
            {
                var data = TempData["Cart"];
                productIds = JsonSerializer.Deserialize<List<List<int>>>(data.ToString());
                productIds.Add(new List<int> { productId, quantity });
                TempData["Cart"] = JsonSerializer.Serialize(productIds);
            }
            else //Nothing is in the cart yet
            {
                var first = new List<int> { productId, quantity };
                productIds.Add(first);
                TempData["Cart"] = JsonSerializer.Serialize(productIds);
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "FoodId", "Name");
            return View(new OrderViewModel());
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FoodId,Name,Price,OrderTime,StoreId,CustomerId")] Orders orders,int quantity,int foodid)
        {
            TempData["Cart"] = null;
            if (ModelState.IsValid)
            {
                orders.Name = _context.Foods.Find(foodid).Name;
                orders.OrderTime = DateTime.Now;
                _context.Add(orders);
                await _context.SaveChangesAsync();
                TempData["LocID"] = orders.StoreId;
                TempData["CustID"] = orders.CustomerId;
                return RedirectToAction("AddOrderItem", new { productId=foodid,quantity=quantity});
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orders.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.StoreLoc, "StoreId", "Name", orders.StoreId);
            ViewData["FoodId"] = new SelectList(_context.StoreLoc, "FoodId", "Name", orders.FoodId);
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
