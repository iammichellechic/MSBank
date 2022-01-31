using MSBank.Infrastracture.Paging;
using MSBank.Models;

namespace MSBank.Services
{
    public interface ICustomerService
    {


        PagedResult<Customer> GetAll(string sortColumn,
            ExtensionMethods.QuerySortOrder sortOrder, int page, string q );

    
    }
}
