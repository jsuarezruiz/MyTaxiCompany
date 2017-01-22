using MyTaxyCompany01.Data;
using MyTaxyCompany01.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyTaxyCompany01.Services.Customers
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