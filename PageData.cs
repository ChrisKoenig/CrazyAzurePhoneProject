using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Linq;
using CrazyAzurePhoneProject.AdventureWorks;

namespace CrazyAzurePhoneProject
{
    public class PageData : INotifyPropertyChanged
    {
        private DataServiceCollection<Product> _products;

        public DataServiceCollection<Product> DSProducts
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                RaisePropertyChanged("DSProducts");
                RaisePropertyChanged("Products");
            }
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
        }

        private AdventureWorksLT2008R2Entities entities;

        public void LoadData()
        {
            var uri = new Uri(App.AWSERVICE);
            entities = new AdventureWorksLT2008R2Entities(uri);
            DSProducts = new DataServiceCollection<Product>(entities);
            var query = entities.Products;
            DSProducts.LoadAsync(query);
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion INPC
    }
}