using MyTaxyCompany01.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyTaxyCompany01.Services.Customers
{
    public interface ICustomersService
    {
        Task<ObservableCollection<Customer>> GetCustomersAsync();
    }
}
