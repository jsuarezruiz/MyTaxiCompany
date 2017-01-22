using Microsoft.Practices.Unity;
using MyTaxyCompany01.Services.Customers;
using MyTaxyCompany01.Services.Navigation;
using System;

namespace MyTaxyCompany01.ViewModels.Base
{
    public class ViewModelLocator
    {
        private readonly IUnityContainer _unityContainer;

        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get { return _instance; }
        }

        protected ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            // Services    
            RegisterServices();

            // View models
            _unityContainer.RegisterType<MainViewModel>();
        }

        private void RegisterServices()
        {
            // Services    
            _unityContainer.RegisterType<ICustomersService, CustomersService>();
            _unityContainer.RegisterType<INavigationService, NavigationService>();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}