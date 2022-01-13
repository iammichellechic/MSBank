using MSBank.Infrastracture.Paging;
using MSBank.Models;

namespace MSBank.Services
{
    public interface ICustomerService
    {

        // List<Customer> GetCustomers(string sortColumn, string sortOrder);

        //add int customerId
        PagedResult<Customer> GetAll(string sortColumn,
            string sortOrder, int page, string q);
    }
}
