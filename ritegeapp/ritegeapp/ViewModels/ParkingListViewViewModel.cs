using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rg.Plugins.Popup.Services;
using ritegeapp.Services;
using RitegeDomain.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class ParkingListViewViewModel : ObservableObject
    {
        IDataService dataService;

        public ParkingListViewViewModel(ObservableObject viewmodel,string viewName)
        {
            dataService = DependencyService.Get<IDataService>();
            ViewName = viewName;
            parkingList = new();
            parentvm = (ObservableObject)viewmodel;
        }
        public ObservableObject parentvm;
        public string ViewName;
        [ObservableProperty]
        private ObservableCollection<ParkingData> parkingList=new();
        [ObservableProperty]
        private bool isLoading = true;
        [ObservableProperty]
        private bool showData = false;
        [ICommand]
        private async void BackgroundClicked(object parameter)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        [ICommand]
        private async void ParkingClicked(object parameter)
        {
            if(ViewName=="Dashboard")
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "ParkingClicked", ((ParkingData)parameter));
            }
            else
                if (ViewName=="Ticket")
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "TicketViewParkingClicked", ((ParkingData)parameter));
            await PopupNavigation.Instance.PopAllAsync();
        }
        public async void LoadList()
        {
            IsLoading = true; showData = false;
            Dictionary<int,string> list=new();
            if (ViewName == "Dashboard")
            {
                list = ((TableauDeBordViewModel)parentvm).ParkingList;
            }
            else if (ViewName == "Ticket")
            {
                list = ((GestionRecettesViewModel)parentvm).ParkingList;
            }

            if (list is not null && list.Count>0)
            foreach (var parking in list)
            {
                await Device.InvokeOnMainThreadAsync(() => ParkingList.Add(new ParkingData(parking.Key,parking.Value)));
            }
            IsLoading = false; showData = true;
        }
    }
}
