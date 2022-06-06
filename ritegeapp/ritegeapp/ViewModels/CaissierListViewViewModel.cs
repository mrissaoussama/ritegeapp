using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rg.Plugins.Popup.Services;
using RitegeDomain.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class CaissierListViewViewModel : ObservableObject
    {
        public CaissierListViewViewModel(ObservableObject viewmodel)
        {
            caisserList = new();
            parentvm = viewmodel;
        }
        public ObservableObject parentvm;
        [ObservableProperty]
        private ObservableCollection<CaissierData> caisserList=new();
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
            Debug.WriteLine(((CaissierData)parameter).CaissierName);
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CaissierClicked", ((ParkingData)parameter).ParkingName);
            }
            await PopupNavigation.Instance.PopAllAsync();
        }
        public async void LoadList()
        {
            IsLoading = true; showData = false;
            var list = (await (Application.Current as App).dataService.GetParkingList());
            if(list is not null && list.Count>0)
            foreach (var caissier in list)
            {
                await Device.InvokeOnMainThreadAsync(() => caisserList.Add(new CaissierData(caissier)));
            }
            IsLoading = false; showData = true;
        }
    }
}
