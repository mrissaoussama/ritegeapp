using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rg.Plugins.Popup.Services;
using ritegeapp.Services;
using RitegeDomain.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class CashRegisterListViewViewModel : ObservableObject
    {
        public CashRegisterListViewViewModel(ObservableObject viewmodel)
        {
            CashRegisterList = new();
            parentvm = viewmodel;
        }
        public ObservableObject parentvm;
        [ObservableProperty]
        private ObservableCollection<CashRegisterData> cashRegisterList=new();
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
        private async void CashRegisterClicked(object parameter)
        {
            Debug.WriteLine(((CashRegisterData)parameter).CashRegisterName);
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CashRegisterClicked", ((CashRegisterData)parameter).CashRegisterName);
            }
            await PopupNavigation.Instance.PopAllAsync();
        }
        public async void LoadList()
        {
            IsLoading = true; showData = false;
            var list = (await DependencyService.Get<IDataService>().GetCashRegisterList(((TableauDeBordViewModel)parentvm).Parking));
            if(list is not null && list.Count>0)
            foreach (var CashRegister in list)
            {
                await Device.InvokeOnMainThreadAsync(() => CashRegisterList.Add(new CashRegisterData(CashRegister)));
            }
            IsLoading = false; showData = true;
        }
    }
}
