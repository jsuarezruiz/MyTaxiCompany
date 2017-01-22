using MyTaxiCompany02.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyTaxiCompany02.Services.Customers
{
    public interface ICustomersService
    {
        Task<ObservableCollection<Customer>> GetCustomersAsync();
    }
}
