using MyTaxiCompany02.Models;
using MyTaxiCompany02.Services.Customers;
using MyTaxiCompany02.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyTaxiCompany02.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _customers;

        private ICustomersService _customersService;

        public MainViewModel(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                RaisePropertyChanged(() => Customers);
            }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            Customers = await _customersService.GetCustomersAsync();
        }
    }
}
