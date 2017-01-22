using CoreLocation;
using MyTaxiCompany02.Models;

namespace MyTaxiCompany02.iOS.Maps
{
    public static class CoordinateConverter
    {
        public static CLLocationCoordinate2D ConvertToNative(Geoposition position)
        {
            return new CLLocationCoordinate2D()
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
        }

        public static Geoposition ConvertToAbstraction(CLLocationCoordinate2D position)
        {
            return new Geoposition
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
        }
    }
}