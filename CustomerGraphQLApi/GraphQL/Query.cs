using System.Linq;
using CustomerGraphQLApi.Models;
using CustomerGraphQLApi.Services;

namespace CustomerGraphQLApi.GraphQL
{
    public class Query
    {

        #region Property  
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor  
        public Query(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        #endregion

        #region Public Methods
        public IQueryable<Customer> Customers () => _customerService.GetList();
        #endregion
    }
}
