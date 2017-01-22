using MyTaxiCompany02.Data;
using MyTaxiCompany02.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyTaxiCompany02.Services.Customers
{
    public class CustomersService : ICustomersService
    {
        public async Task<ObservableCollection<Customer>> GetCustomersAsync()
        {
            await Task.Delay(500);

            return new ObservableCollection<Customer>(DataRepository.LoadCustomerData());
        }
    }
}