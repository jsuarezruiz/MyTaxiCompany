using MyTaxiCompany02.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MyTaxiCompany02.Data
{
    public static class DataRepository
    {
        private static IList<Customer> _customerList;

        public static IList<Customer> LoadCustomerData()
        {
            return _customerList ?? 
                (_customerList = 
                LoadData<IList<Customer>>(GlobalSetting.CustomerJsonDataFile));
        }

        private static T LoadData<T>(string dataFileName)
        {
            var assembly = typeof(DataRepository).GetTypeInfo().Assembly;
            string preparedDataFileName = string.Format("MyTaxiCompany02.{0}",
                dataFileName.Replace("/", "."));
            Stream stream = assembly.GetManifestResourceStream(preparedDataFileName);

            if (stream == null)
            {
                return default(T);
            }

            using (StreamReader sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
            }
        }
    }
}
