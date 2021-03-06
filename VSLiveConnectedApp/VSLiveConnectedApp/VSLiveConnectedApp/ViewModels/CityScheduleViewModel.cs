﻿using System.Linq;
using System.Threading.Tasks;
using VSLiveConnectedApp.Data;
using VSLiveConnectedApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace VSLiveConnectedApp.ViewModels
{
    public class CityScheduleViewModel : ViewModelBase
    {
        private string _cityId;

        public CityScheduleViewModel(string cityId, string cityName)
        {
            _cityId = cityId;
            _cityName = cityName;
        }

        private ObservableCollection<Grouping<string, Slot>> _groupedSlots;
        public ObservableCollection<Grouping<string, Slot>> GroupedSlots
        {
            get
            {
                return _groupedSlots;
            }
            set
            {
                _groupedSlots = value;
                OnPropertyChanged();
            }
        }

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
        }

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
            await GetSchedule(true);
            IsLoading = false;
        }

        public async Task GetSchedule(bool isUserInitiated)
        {
            var client = new ServiceClient();
            var schedule = await client.GetScheduleForCity(_cityId, isUserInitiated);

            if (schedule != null)
            {
                await Task.Run(() => from slot in schedule.Slots
                                     orderby slot.StartTime
                                     group slot by slot.DayFormatted
                    into slotGroup
                                     select new Grouping<string, Slot>(slotGroup.Key, slotGroup)).ContinueWith(r =>
                                     {
                                         GroupedSlots = new ObservableCollection<Grouping<string, Slot>>(r.Result);
                                     });
            }
        }
    }
}
