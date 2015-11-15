using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLiveConnectedApp.Data;
using VSLiveConnectedApp.ViewModels;
using Xamarin.Forms;

namespace VSLiveConnectedApp.Views
{
    public partial class CityDetailView : ContentPage
    {
        private CityDetailViewModel ViewModel
        {
            get
            {
                return BindingContext as CityDetailViewModel;
            }
            set { BindingContext = value; }
        }

        public CityDetailView()
        {
            InitializeComponent();
        }

        public CityDetailView(City city): this()
        {
            ViewModel = new CityDetailViewModel(this, city);
        }
    }
}
