using MapKit;
using MyTaxiCompany02.Models;

namespace MyTaxiCompany02.iOS.Maps.Annotations
{
    public class CustomerAnnotationView : MKAnnotationView
    {
        public const string CustomReuseIdentifier = nameof(CustomerAnnotationView);

        private Customer _customer;

        public Customer Customer
        {
            get
            {
                return _customer;
            }

            set
            {
                _customer = value;
                UpdateImage();
            }
        }

        public CustomerAnnotationView(IMKAnnotation annotation, Customer customer)
            : base(annotation, CustomReuseIdentifier)
        {
            Customer = customer;
        }

        private void UpdateImage()
        {
            switch (Customer.CustomerCategory)
            {
                case CustomerType.Business:
                    Image = AnnotationImages.BusinessImage;
                    break;
                case CustomerType.Group:
                    Image = AnnotationImages.GroupImage;
                    break;
                default:
                    Image = AnnotationImages.AnonymousImage;
                    break;
            }
        }
    }
}