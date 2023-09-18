using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Data.DAL.Cadastros
{
    public class CustomerDAL
    {
        private IESContext _context;
        public CustomerDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetCustomersByName()
        {
            return _context.Customer.OrderBy(c => c.Name);
        }

        public async Task<Customer> GetCustomerById(long Id)
        {
            return await _context.Customer.FindAsync(Id);
        }

        public async Task<Customer>SaveCustomer(Customer customer)
        {
            if(customer.CustomerId == 0)
            {
                _context.Customer.Add(customer);
            }
            else
            {
                _context.Customer.Update(customer);
            }

            await _context.SaveChangesAsync();
            
            return customer;
        }

        public async Task<Customer> DeleteCustomer(long id)
        {
            Customer customer = await GetCustomerById(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            
            return customer;
        }
    }
}
