using MyTaxiCompany02.Models;
using MyTaxiCompany02.Controls;
using System.Collections.Generic;

namespace MyTaxiCompany02.Maps
{
    public abstract class AbstractPushpinManager
    {
        protected readonly CustomMap FormsMap;

        protected AbstractPushpinManager(CustomMap formsMap)
        {
            FormsMap = formsMap;
        }

        public void AddCustomers(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                AddCustomerToMap(customer);
            }
        }

        public abstract void RemoveAllCustomers();

        public abstract void RemoveCustomers(IEnumerable<Customer> removedCustomers);

        protected abstract void AddCustomerToMap(Customer customer);
    }
}