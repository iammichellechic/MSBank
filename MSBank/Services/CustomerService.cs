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
        public PagedResult<Customer> GetAll(string sortColumn, string sortOrder, int page, string q)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                        .Where(r => q == null || r.Givenname.Contains(q) || r.Surname.Contains(q) || r.City.Contains(q) || r.CustomerId.ToString().Contains(q));
            }

            if (sortColumn == "Customer ID")
                if (sortOrder == "asc")
                    query = query.OrderBy(r => r.CustomerId);
                else
                    query = query.OrderByDescending(r => r.CustomerId);

            else if (sortColumn == "First Name" || string.IsNullOrEmpty(sortColumn))
                if (sortOrder == "desc")
                    query = query.OrderByDescending(r => r.Givenname);
                else
                    query = query.OrderBy(r => r.Givenname);

            else if (sortColumn == "Last Name" || string.IsNullOrEmpty(sortColumn))
                if (sortOrder == "desc")
                    query = query.OrderByDescending(r => r.Surname);
                else
                    query = query.OrderBy(r => r.Surname);

            else if (sortColumn == "Personal Number" || string.IsNullOrEmpty(sortColumn))
                if (sortOrder == "desc")
                    query = query.OrderByDescending(r => r.NationalId);
                else
                    query = query.OrderBy(r => r.NationalId);

            else if (sortColumn == "Street Address" || string.IsNullOrEmpty(sortColumn))
                if (sortOrder == "desc")
                    query = query.OrderByDescending(r => r.Streetaddress);
                else
                    query = query.OrderBy(r => r.Streetaddress);

            else if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(r => r.City);
                else
                    query = query.OrderByDescending(r => r.City);

            return query.GetPaged(page, 50);
        }
   
        // return query.ToList();
    }

}
