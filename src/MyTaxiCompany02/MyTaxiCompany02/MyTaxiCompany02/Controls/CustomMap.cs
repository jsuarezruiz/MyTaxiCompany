using MyTaxiCompany02.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyTaxiCompany02.Controls
{
    public class CustomMap : Map
    {
        public CustomMap()
        {
            Initialize();
        }

        public static readonly BindableProperty CustomersProperty =
            BindableProperty.Create("Customers",
                typeof(IEnumerable<Customer>), typeof(CustomMap), default(IEnumerable<Customer>),
                BindingMode.TwoWay);

        public IEnumerable<Customer> Customers
        {
            get { return (IEnumerable<Customer>)base.GetValue(CustomersProperty); }
            set { base.SetValue(CustomersProperty, value); }
        }

        private void Initialize()
        {
            MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(GlobalSetting.UserLatitude, GlobalSetting.UserLongitude),
                Distance.FromMiles(GlobalSetting.ZoomLevel)));
        }
    }
}