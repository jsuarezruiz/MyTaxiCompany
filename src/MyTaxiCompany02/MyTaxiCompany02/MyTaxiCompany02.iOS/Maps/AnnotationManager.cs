using CoreLocation;
using MapKit;
using MyTaxiCompany02.Controls;
using MyTaxiCompany02.iOS.Maps.Annotations;
using MyTaxiCompany02.Maps;
using MyTaxiCompany02.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyTaxiCompany02.iOS.Maps
{
    public class AnnotationManager : AbstractPushpinManager
    {
        private readonly MKMapView _nativeMap;

        public AnnotationManager(MKMapView nativeMap, CustomMap formsMap)
            : base(formsMap)
        {
            _nativeMap = nativeMap;
        }   

        public MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is CustomerAnnotation)
            {
                return GetViewForCustomerAnnotation(annotation as CustomerAnnotation);
            }     
            else
            {
                return null;
            }
        }

        public override void RemoveAllCustomers()
        {
            CustomerAnnotation[] annotations =
                _nativeMap?.Annotations?.OfType<CustomerAnnotation>()
                                        .ToArray();

            if (annotations != null)
            {
                _nativeMap.RemoveAnnotations(annotations);
            }
        }

        public override void RemoveCustomers(IEnumerable<Customer> removedCustomers)
        {
            CustomerAnnotation[] annotations =
                _nativeMap?.Annotations?.OfType<CustomerAnnotation>()
                                        .Where(a => removedCustomers.Any(i => i.Id == a.Customer.Id))
                                        .ToArray();

            if (annotations != null)
            {
                _nativeMap.RemoveAnnotations(annotations);
            }
        }

        protected override void AddCustomerToMap(Customer customer)
        {
            var annotation = new CustomerAnnotation(new
                     CLLocationCoordinate2D
            {
                Latitude = customer.Latitude,
                Longitude = customer.Longitude
            },  
            customer);

            _nativeMap.AddAnnotation(annotation);
        }

        private MKAnnotationView GetViewForCustomerAnnotation(CustomerAnnotation annotation)
        {
            var annotationView = _nativeMap.DequeueReusableAnnotation(CustomerAnnotationView.CustomReuseIdentifier) as CustomerAnnotationView;

            if (annotationView == null)
            {
                annotationView = new CustomerAnnotationView(annotation, annotation.Customer);
            }
            else
            {
                annotationView.Customer = annotation.Customer;
            }

            return annotationView;
        }
    }
}