using brendan_project1.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brendan_project1.DataAccess.Repositories
{
    class OrdersRepo : IOrdersRepo
    {
        readonly RestaurantAfrikContext context = new RestaurantAfrikContext();
        public void AddOrderItem(Foods food)
        {

        }
        public Orders FindByID(int id)
        {
            return context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Store)
                    .Include("OrderFoods.F")
                    .Include("OrderFoods.F.F")
                    .FirstOrDefault(m => m.OrderId == id);
        }

        public void Remove(int id)
        {
            var toRemove = context.Orders.Find(id);
            context.Remove(toRemove);
            context.SaveChanges();
        }

        public IEnumerable<StoreLoc> GetLocs()
        {
            return context.StoreLoc;
        }

        public async Task<IEnumerable<Orders>> GetOrds()
        {
            return await context.Orders.Include(o => o.Customer).Include(o => o.Store).ToListAsync();
        }

        /// <summary>
        /// Adds an order to database
        /// </summary>
        /// <param name="cust"></param>
        public int Add(Orders o)
        {
            context.Orders.Add(o);
            context.SaveChanges();
            context.Entry(o).Reload();
            return o.OrderId;
        }

        /// <summary>
        /// Sets order's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Orders o)
        {
            context.Entry(o).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Returns price of added item


        public async Task<int> ValidateOrder(int id)
        {
            var ordList = await GetOrders();
            foreach (var order in ordList)
            {
                if (id > 0 && order.OrderId == id)
                {
                    return id;
                }

            }
            return -1;
        }

        public int CreateOrder(int custid, int Locid)
        {
            var new_order = new Orders
            {
                CustomerId = custid,
                StoreId = Locid,
                //Total = 0,
                //Timestamp = DateTime.Now,
            };
            context.Orders.Add(new_order);
            context.SaveChanges();
            return new_order.OrderId;
        }

        //Searches orders by given param, param is checked against Order columns according to mode
        //Mode Codes:
        //  1: Get orders by location
        //  2: By customer
        //  3: Get details of 1 specific order
        public async Task<List<Orders>> GetOrders(int mode = 0, params string[] search_param)
        {
            var orderList = context.Orders.Include("StoreLoc").Include("Customer").AsQueryable();
            using (var context = new RestaurantAfrikContext())
            {
                switch (mode)
                {
                    case 1:
                        orderList = orderList
                        .Where(o => o.Store.Name == search_param[0]);
                        break;
                    case 2:
                        orderList = orderList
                        .Where(o => o.Customer.FirstName == search_param[0] && o.Customer.LastName == search_param[1]);
                        break;
                    case 3:
                        orderList = orderList
                        .Where(o => o.OrderId == Convert.ToInt32(search_param[0]));
                        break;
                    default:
                        break;
                }
            }
            return await orderList
                        .Include("OrderFoods")
                        .Include("OrderFoods.F")
                        .Include("OrderFoods.F.F")
                        .ToListAsync();
        }
    }
}
