using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerGraphQLApi.Data;
using CustomerGraphQLApi.Models;
using CustomerGraphQLApi.ErrorHandling;
using Microsoft.Extensions.Logging;

namespace CustomerGraphQLApi.Services
{
    public class CustomerService : ICustomerService
    {
        #region Property
        private readonly CustomerContext _dbContext;
        public ILogger Logger { get; set; }
        #endregion

        #region Constructor
        public CustomerService(CustomerContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        #endregion

        #region Public Methods
        public async Task<Customer> Create(AddCustomer input)
        {
            try
            {
                var data = new Customer()
                {
                    Email = input.Email,
                    Name = input.Name,
                    Code = input.Code,
                    Status = input.Status,
                    IsBlocked = input.IsBlocked,
                };

                await _dbContext.Customers.AddAsync(data);
                await _dbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return default;
            }
            
        }

        public async Task<bool> DeleteById(DeleteCustomerById deleteCustomerById)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == deleteCustomerById.Id);

                if (customer == null)
                {
                    throw new CustomerNotFoundException() { CustomerId = deleteCustomerById.Id };
                }
                else
                {
                    _dbContext.Customers.Remove(customer);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomerNotFoundException)
                    throw;

               
                return false;
            }
        }

        public IQueryable<Customer> GetList()
        {
            try
            {
                return _dbContext.Customers.AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return Enumerable.Empty<Customer>().AsQueryable();
            }
        }

        public async Task<bool> Update(Customer customer)
        {
            try
            {
                var customerToUpdate = await _dbContext.Customers.FirstOrDefaultAsync(s => s.Id == customer.Id);

                if (customerToUpdate == null)
                {
                    throw new CustomerNotFoundException() { CustomerId = customer.Id };
                }
                else
                {
                    customerToUpdate.Email = customer.Email;
                    customerToUpdate.Name = customer.Name;
                    customerToUpdate.Code = customer.Code;
                    customerToUpdate.Status = customer.Status;
                    customerToUpdate.IsBlocked = customer.IsBlocked;

                    _dbContext.Update(customerToUpdate);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                if ((ex is CustomerNotFoundException))
                    throw;

                Logger.LogError(ex.Message);

            }
            return false;
        }
    }
    #endregion
}

