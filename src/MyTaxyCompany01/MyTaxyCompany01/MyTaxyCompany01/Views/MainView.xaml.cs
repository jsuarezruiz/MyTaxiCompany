using MyTaxyCompany01.Data;
using MyTaxyCompany01.ViewModels;
using MyTaxyCompany01.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyTaxyCompany01.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.Instance.Resolve<MainViewModel>();
            BindingContext = viewModel;

            /*
            AddPins();
            PositionMap();
            */
        }

        private void AddPins()
        {
            foreach (var customer in DataRepository.LoadCustomerData())
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(customer.Latitude, customer.Longitude),
                    Label = customer.Name,
                    Address = customer.Address
                };

                MyMap.Pins.Add(pin);
            }
        }

        private void PositionMap()
        {
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(GlobalSetting.UserLatitude, GlobalSetting.UserLongitude),
                    Distance.FromMiles(1)));
        }
    }
}