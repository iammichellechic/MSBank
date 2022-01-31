using MSBank.Infrastracture.Paging;
using MSBank.Models;


namespace MSBank.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly BankAppDataContext _context;
        public CustomerService(BankAppDataContext context)

        {
            _context = context;
        }
        public PagedResult<Customer> GetAll(string sortColumn, ExtensionMethods.QuerySortOrder sortOrder, int page, string q)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                        .Where(r => q == null || r.CustomerId.ToString().Contains(q) || r.Givenname.Contains(q) || r.Surname.Contains(q) || r.City.Contains(q));

            }
             if (string.IsNullOrEmpty(sortColumn))
                    sortColumn = nameof(Customer.CustomerId);


            else if (string.IsNullOrEmpty(sortColumn))
                sortColumn = nameof(Customer.Givenname);

            else if (string.IsNullOrEmpty(sortColumn))
                sortColumn = nameof(Customer.Surname);

            else if (string.IsNullOrEmpty(sortColumn))
                sortColumn = nameof(Customer.NationalId);

            else if (string.IsNullOrEmpty(sortColumn))
                sortColumn = nameof(Customer.Streetaddress);

            else if (string.IsNullOrEmpty(sortColumn))
                sortColumn = nameof(Customer.City);

    

            query = query.OrderBy(sortColumn, sortOrder);

                return query.GetPaged(page, 50);
            }

       
    }
 

}

