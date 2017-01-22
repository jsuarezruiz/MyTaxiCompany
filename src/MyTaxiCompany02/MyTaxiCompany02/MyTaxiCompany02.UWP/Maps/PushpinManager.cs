using MyTaxiCompany02.Controls;
using MyTaxiCompany02.Maps;
using MyTaxiCompany02.Models;
using MyTaxiCompany02.UWP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;

namespace MyTaxiCompany02.UWP.Maps
{
    public class PushpinManager : AbstractPushpinManager
    {
        private readonly MapControl _nativeMap;
        private readonly MapItemsControl _mapItems;
        private readonly Dictionary<MapIcon, int> _pushpinMappings;

        public PushpinManager(MapControl nativeMap, CustomMap formsMap) 
            : base(formsMap)
        {
            _nativeMap = nativeMap;
            _mapItems = new MapItemsControl();
            _nativeMap.Children.Add(_mapItems);
            _pushpinMappings = new Dictionary<MapIcon, int>();
        }

        protected override async void AddCustomerToMap(Customer customer)
        {
            var geoLocation = CoordinateConverter.ConvertToNative(customer.GeoLocation);

            var mapIcon = new MapIcon();
            mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
            mapIcon.Location = geoLocation;
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon.ZIndex = 1000;

            var iconImageUri = default(Uri);

            switch (customer.CustomerCategory)
            {
                case CustomerType.Business:
                    iconImageUri = new Uri("ms-appx:///Assets/business.png");
                    break;
                case CustomerType.Group:
                    iconImageUri = new Uri("ms-appx:///Assets/group.png");
                    break;
                default:
                    iconImageUri = new Uri("ms-appx:///Assets/anonymous.png");
                    break;
            }

            RandomAccessStreamReference stream = RandomAccessStreamReference.CreateFromUri(iconImageUri);
            mapIcon.Image = await stream.ScaleTo(48, 48);

            _nativeMap.MapElements.Add(mapIcon);
            _pushpinMappings.Add(mapIcon, customer.Id);
        } 
           
        private Models.Geoposition GetIconPosition(DependencyObject icon)
        {
            if (icon == null)
            {
                return default(Models.Geoposition);
            }

            Geopoint geoLocation = MapControl.GetLocation(icon);

            return CoordinateConverter.ConvertToAbstraction(geoLocation);
        }

        private void SetMapIconPosition(DependencyObject icon, Models.Geoposition geoLocation, Point anchorPoint)
        {
            var nativeCoordinate = CoordinateConverter.ConvertToNative(geoLocation);

            MapControl.SetLocation(icon, nativeCoordinate);
            MapControl.SetNormalizedAnchorPoint(icon, anchorPoint);
        }
       
        public override void RemoveAllCustomers()
        {
            RemoveCustomersIcons(_pushpinMappings.Keys.ToList());
        }

        public override void RemoveCustomers(IEnumerable<Customer> removedCustomers)
        {
            var iconsToRemove = _pushpinMappings.Where(x => removedCustomers.Any(i => i.Id == x.Value))
                                                .Select(x => x.Key)
                                                .ToList();

            RemoveCustomersIcons(iconsToRemove);
        }

        private void RemoveCustomersIcons(List<MapIcon> icons)
        {
            if (icons != null)
            {
                foreach (var icon in icons)
                {
                    _nativeMap?.MapElements?.Remove(icon);
                    _pushpinMappings.Remove(icon);
                }
            }
        }

        private void RemoveIcons(IEnumerable<DependencyObject> icons)
        {
            if (icons != null)
            {
                foreach (var icon in icons)
                {
                    _mapItems?.Items?.Remove(icon);
                }
            }
        }
    }
}
