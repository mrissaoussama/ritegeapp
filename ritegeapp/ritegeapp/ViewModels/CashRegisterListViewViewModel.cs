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
            cashRegisterList = new();
            parentvm = (TableauDeBordViewModel)viewmodel;
        }
        public TableauDeBordViewModel parentvm;
        [ObservableProperty]
        private ObservableCollection<CashRegisterData> cashRegisterList=new();
        [ObservableProperty]
        private bool isLoading = true;
        [ObservableProperty]
        private bool showData = false;
        [RelayCommand]
        private async void BackgroundClicked(object parameter)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
        [RelayCommand]
        private async void CashRegisterClicked(object parameter)
        {
            Debug.WriteLine(((CashRegisterData)parameter).CashRegisterName);
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CashRegisterClicked", ((CashRegisterData)parameter));
            }
            await PopupNavigation.Instance.PopAllAsync();
        }
        public async void LoadList()
        {
            IsLoading = true; showData = false;

            if (parentvm.CashRegisterList is not null && parentvm.CashRegisterList.Count > 0)
            {
                foreach (var CashRegister in parentvm.CashRegisterList)
                {
                    await Device.InvokeOnMainThreadAsync(() => CashRegisterList.Add(new CashRegisterData(CashRegister.Key, CashRegister.Value)));
                }
            }
            IsLoading = false; showData = true;
        }
    }
}
