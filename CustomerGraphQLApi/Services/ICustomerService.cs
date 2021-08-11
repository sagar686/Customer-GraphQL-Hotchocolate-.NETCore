using CustomerGraphQLApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerGraphQLApi.Services
{
    public interface ICustomerService
    {
        Task<Customer> Create(AddCustomer customer);
        Task<bool> Update(Customer customer);
        Task<bool> DeleteById(DeleteCustomerById deleteCustomerById);
        IQueryable<Customer> GetList();
    }
}
