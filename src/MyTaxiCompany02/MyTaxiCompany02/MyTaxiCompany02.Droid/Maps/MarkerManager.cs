using System.Collections.Generic;
using System.Linq;
using MyTaxiCompany02.Maps;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using MyTaxiCompany02.Controls;
using MyTaxiCompany02.Models;
using MyTaxiCompany02.Droid.Maps.Icons;

namespace MyTaxiCompany02.Droid.Maps
{
    public class MarkerManager : AbstractPushpinManager
    {
        private readonly Dictionary<int, Marker> _customerPushpinMappings;
        private GoogleMap _nativeMap;
        private bool _isUserInteractionEnabled;

        public MarkerManager(GoogleMap nativeMap, CustomMap formsMap)
            : base(formsMap)
        {
            _nativeMap = nativeMap;
            _customerPushpinMappings = new Dictionary<int, Marker>();
            _isUserInteractionEnabled = true;
        }

        protected override void AddCustomerToMap(Customer customer)
        {
            var customerIcon = new CustomerIcon(customer);

            var markerOptions = customerIcon.MarkerOptions;
            markerOptions.SetPosition(new LatLng(customer.Latitude, customer.Longitude));

            Marker marker = _nativeMap.AddMarker(markerOptions);
            _customerPushpinMappings.Add(customer.Id, marker);
        }

        public override void RemoveAllCustomers()
        {
            List<Marker> allMarkers = _customerPushpinMappings.Select(m => m.Value)
                                                              .ToList();
            _customerPushpinMappings.Clear();

            foreach (Marker marker in allMarkers)
            {
                marker.Remove();
            }
        }

        public override void RemoveCustomers(IEnumerable<Customer> removedCustomers)
        {       
            List<KeyValuePair<int, Marker>> entriesToRemove =
                _customerPushpinMappings.Where(x => removedCustomers.Any(i => i.Id == x.Key))
                                        .ToList();

            foreach (KeyValuePair<int, Marker> entry in entriesToRemove)
            {
                entry.Value.Remove();
                _customerPushpinMappings.Remove(entry.Key);
            }
        }

        private KeyValuePair<int, Marker> GetCustomerMappedMarker(Marker marker)
        {
            return _customerPushpinMappings.FirstOrDefault(m => m.Value.Id == marker.Id);
        }  
    }
}