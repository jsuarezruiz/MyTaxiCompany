using System;

namespace MyTaxiCompany02.Models
{
    public class Customer
    {
        private CustomerType _customerType;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime? FullyAttendedTime { get; set; }
        public Geoposition GeoLocation => new Geoposition { Latitude = Latitude, Longitude = Longitude };
       
        public CustomerStatus CurrentStatus
        {
            get
            {
                return CustomerStatus.Resolving;
            }
        }

        public CustomerType CustomerCategory
        {
            get { return _customerType; }
            set { _customerType = value; }
        }
    }
}