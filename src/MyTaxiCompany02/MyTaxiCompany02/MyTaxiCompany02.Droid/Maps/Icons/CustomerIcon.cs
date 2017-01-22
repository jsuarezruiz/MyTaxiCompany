using Android.Gms.Maps.Model;
using MyTaxiCompany02.Models;

namespace MyTaxiCompany02.Droid.Maps.Icons
{
    public class CustomerIcon : BaseIcon
    {
        private const int BusinessResource = Resource.Drawable.business;
        private const int GroupResource = Resource.Drawable.group;
        private const int AnonymousResource = Resource.Drawable.anonymous;

        public CustomerIcon(Customer customer)
            : base()
        {
            Customer = customer;

            Initialize();
        }

        public Customer Customer { get; }

        private void Initialize()
        {
            switch (Customer.CustomerCategory)
            {
                case CustomerType.Business:
                    var businessIcon = BitmapDescriptorFactory.FromResource(BusinessResource);
                    MarkerOptions.SetIcon(businessIcon);
                    break;
                case CustomerType.Group:
                    var groupIcon = BitmapDescriptorFactory.FromResource(GroupResource);
                    MarkerOptions.SetIcon(groupIcon);
                    break;
                default:
                    var anonymousIcon = BitmapDescriptorFactory.FromResource(AnonymousResource);
                    MarkerOptions.SetIcon(anonymousIcon);
                    break;
            }
        }
    }
}