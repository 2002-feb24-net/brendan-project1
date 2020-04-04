using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using brendan_project1;

namespace brendan_project1.Domain.Interfaces
{
    
    public interface ICustomerRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id);

        /// <summary>
        /// Searches custs by id, returns null if not foudn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customers FindByID(int id);

        /// <summary>
        /// Returns ienumerable of custs
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Customers>> GetCusts();

        /// <summary>
        /// Adds a customer to database
        /// </summary>
        /// <param name="cust"></param>
        public void Add(Customers cust);

        /// <summary>
        /// Sets customer's state to edited
        /// </summary>
        /// <param name="cust"></param>
        public void Edit(Customers cust);

        /// <summary>
        /// Returns number of customers
        /// </summary>
        /// <returns></returns>
        int NumberOfCustomers();
      
        /// <summary>
        /// Adds customer to databse
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastNumber">Last name</param>
        /// <param phone="phoneNumber"></param>
        
        int AddCust(string firstName, string lastName, int phoneNumber);

        /// <summary>
        /// Searches customers by given parameter/search mode
        /// </summary>
        /// <param name="mode">Search mode: 1 - By name, 2 - By username</param>
        /// <param name="search_param">name/username to search by</param>
        /// <returns></returns>
        IEnumerable<Customers> SearchCust(int mode = 0, params string[] search_param);

        //returns -1 if customer name or id does not exist
        //returns the matching ID other wise
        /// <summary>
        /// Verifies if customer with given name or id exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>id corresponding to customer matching search param or -1 if not exists</returns>
      
       
        List<Customers> GetList();
 
    }
    
}
