using MyTaxiCompany02.Controls;
using MyTaxiCompany02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms.Maps;

namespace MyTaxiCompany02.Maps
{
    public class MapManager
    {
        private readonly CustomersObserver _customersObserver;

        private bool _mapAlreadyCentered;

        public AbstractPushpinManager PushpinManager { get; private set; }

        public CustomMap FormsMap { get; private set; }

        public MapManager(
            CustomMap formsMap,
            AbstractPushpinManager pushpinManager)
        {
            FormsMap = formsMap;
            PushpinManager = pushpinManager;

            _mapAlreadyCentered = false;
            _customersObserver = new CustomersObserver(this);
        }

        public void Initialize()
        {

        }

        public void HandleCustomMapPropertyChange(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(CustomMap.CustomersProperty.PropertyName, StringComparison.CurrentCultureIgnoreCase))
            {
                _customersObserver.AttachCustomers(FormsMap.Customers);

                if (!_mapAlreadyCentered && FormsMap.Customers?.Any() == true)
                {
                    InitializeMapPosition();
                    _mapAlreadyCentered = true;
                }
            }
        }

        private void InitializeMapPosition()
        {
            IEnumerable<Customer> customers = FormsMap?.Customers;

            if (customers?.Any() == false)
            {
                return;
            }

            var centerPosition = new Position(customers.Average(x => x.Latitude),
                customers.Average(x => x.Longitude));

            var minLongitude = customers.Min(x => x.Longitude);
            var minLatitude = customers.Min(x => x.Latitude);

            var maxLongitude = customers.Max(x => x.Longitude);
            var maxLatitude = customers.Max(x => x.Latitude);

            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude,
                maxLatitude, maxLongitude, 'M') / 2;

            FormsMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
        }
    }
}