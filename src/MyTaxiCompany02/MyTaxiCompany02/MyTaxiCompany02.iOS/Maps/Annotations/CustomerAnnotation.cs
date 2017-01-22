using CoreLocation;
using MyTaxiCompany02.Models;

namespace MyTaxiCompany02.iOS.Maps.Annotations
{
    public class CustomerAnnotation : BaseAnnotation
    {
        public Customer Customer { get; private set; }

        public CustomerAnnotation(CLLocationCoordinate2D coordinate, Customer customer)
            : base(coordinate)
        {
            Customer = customer;
        }
    }
}