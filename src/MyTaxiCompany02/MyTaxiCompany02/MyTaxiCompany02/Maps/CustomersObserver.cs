using MyTaxiCompany02.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace MyTaxiCompany02.Maps
{
    internal class CustomersObserver
    {
        private readonly MapManager _mapManager;
        private IEnumerable<Customer> _customers;

        public CustomersObserver(MapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public void AttachCustomers(IEnumerable<Customer> customers)
        {
            _mapManager.PushpinManager.RemoveAllCustomers();

            INotifyCollectionChanged currentCollection = _customers as INotifyCollectionChanged;

            if (currentCollection != null)
            {
                currentCollection.CollectionChanged -= OnCustomersCollectionChanged;
            }

            _customers = customers;

            if (_customers != null)
            {
                _mapManager.PushpinManager.AddCustomers(_customers);
            }

            if (currentCollection != null)
            {
                currentCollection.CollectionChanged -= OnCustomersCollectionChanged;
            }
        }

        private void OnCustomersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                IEnumerable<Customer> newCustomers = e.NewItems.OfType<Customer>();
                _mapManager.PushpinManager.AddCustomers(newCustomers);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                IEnumerable<Customer> removedCustomers = e.OldItems.OfType<Customer>();
                _mapManager.PushpinManager.RemoveCustomers(removedCustomers);
            }
        }
    }
}