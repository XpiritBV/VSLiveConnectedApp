using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VSLiveConnectedApp.Data;
using VSLiveConnectedApp.Services;
using Xamarin.Forms;

namespace VSLiveConnectedApp.ViewModels
{
    class CitiesViewModel: ViewModelBase
    {
        private Command refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return refreshCommand ??
                    (refreshCommand = new Command(async () => await ExecuteRefreshCommand(), () => !IsLoading));
            }
        }

        private async Task ExecuteRefreshCommand()
        {
            IsLoading = true;
            await GetCities();
            IsLoading = false;
        }

        public async Task GetCities()
        {
            var client = new ServiceClient();
            Cities = await client.GetCities();
        }

        private IEnumerable<City> _cities;
        public IEnumerable<City> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }
    }
}
