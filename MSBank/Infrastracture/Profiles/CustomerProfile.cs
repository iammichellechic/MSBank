using AutoMapper;

namespace MSBank.Infrastracture.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Models.Customer, MSBank.Pages.Customer.EditModel>()
                    .ReverseMap();

            CreateMap<Models.Customer, MSBank.ViewModels.CustomerViewModel>()
                    .ReverseMap();
        }
    }
}
