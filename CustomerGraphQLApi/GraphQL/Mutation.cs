using System.Threading.Tasks;
using CustomerGraphQLApi.Models;
using CustomerGraphQLApi.Services;

namespace CustomerGraphQLApi.GraphQL
{
    public class Mutation
    {
        #region Property  
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor  
        public Mutation(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Public Methods
        public async Task<Customer> Create(AddCustomer input) => await _customerService.Create(input);
        public async Task<bool> DeleteById(DeleteCustomerById deleteCustomerById) => await _customerService.DeleteById(deleteCustomerById);
        public async Task<bool> Update(Customer customer) => await _customerService.Update(customer);
        #endregion


    }
    



}


