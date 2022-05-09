using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rg.Plugins.Popup.Services;
using RitegeDomain.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class EventListViewViewModel : ObservableObject
    {
        public EventListViewViewModel(ObservableObject viewmodel)
        {
            EventList = new();
            parentvm = viewmodel;
        }
        public ObservableObject parentvm;
        [ObservableProperty]
        private ObservableCollection<ParkingEvent> eventList = new();
        [ObservableProperty]
        private bool isLoading = true;
        [ObservableProperty]
        private bool showData = false;
        // private ObservableCollection<string> EventList;

        [ICommand]
        private async void BackgroundClicked(object parameter)
        {
            await PopupNavigation.Instance.PopAllAsync();

        }


        public async Task LoadList()
        {
            IsLoading = true; showData = false;
            foreach (var Event in ((TableauDeBordViewModel)parentvm).eventList)
            {
                await Task.Run(async()=>await Device.InvokeOnMainThreadAsync(() => eventList.Add(Event)));
            }
            IsLoading = false; showData = true;
        }

        #region variables


        #endregion
    }
}
