using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace brendan_project1.Domain.Interfaces
{
    public interface IOrdersRepo
    {
       
        /// <summary>
        /// Removes orders by id
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id);

        /// <summary>
        /// Searches ords by id, returns null if not foudn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Orders FindByID(int id);

        /// <summary>
        /// Returns ienumerable of ords
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Orders>> GetOrds();

        /// <summary>
        /// Adds an order to database
        /// </summary>
        /// <param name="cust"></param>
        /// <returns>id of added order</returns>
        public int Add(Orders ord);

        /// <summary>
        /// Sets order's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Orders ord);

        /// <summary>
        /// Calls getorders, list of orders searched for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>-1 if order does not exist, order id otherwise</returns>
        Task<int> ValidateOrder(int id);
        /// <summary>
        /// Runs sp_create_ord and assisgns cid, lid params
        /// </summary>
        /// <param name="custid"> customer id</param>
        /// <param name="Locid"> location id</param>
        /// <returns>id of created order</returns>
        int CreateOrder(int custid, int Locid);
        /// <summary>
        /// Adds order item to database 
        /// </summary>
        /// <param name="oid">order id</param>
        /// <param name="fid">product id</param>
        /// <param name="qty">quantity </param>
        void AddOrderItem(Foods item);

        /// <summary>
        /// Retrieves all orders from db by given search parameter and id
        /// </summary>
        /// <param name="search_param">location/order/customer id/name</param>
        /// <param name="mode">1: orders for a location, 2: orders for a given customer, 3: specific order, default: all orders
        /// </param>
        /// <returns></returns>
        Task<List<Orders>> GetOrders(int mode = 0, params string[] search_param);
        IEnumerable<Customers> SearchOrders(params string[] search_param);
    }
}
