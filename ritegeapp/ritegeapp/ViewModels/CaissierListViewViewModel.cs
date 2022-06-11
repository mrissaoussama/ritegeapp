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
    public partial class CaissierListViewViewModel : ObservableObject
    {
        public CaissierListViewViewModel(ObservableObject viewmodel)
        {
            CaissierList = new();
            parentvm = (GestionDesSessionsCaissiersViewModel)viewmodel;
        }
        public GestionDesSessionsCaissiersViewModel parentvm;
        [ObservableProperty]
        private ObservableCollection<CaissierData> caissierList=new();
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
        private async void CaissierClicked(object parameter)
        {
            Debug.WriteLine(((CaissierData)parameter).CaissierName);
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "CaissierClicked", ((CaissierData)parameter));
            }
            await PopupNavigation.Instance.PopAllAsync();
        }
        public async void LoadList()
        {
            IsLoading = true; showData = false;

            if (parentvm.ListCaissier is not null && parentvm.ListCaissier.Count > 0)
            {
                await Device.InvokeOnMainThreadAsync(() => CaissierList.Add(new CaissierData(0, "—")));


                foreach (var Caissier in parentvm.ListCaissier)
                {
                    await Device.InvokeOnMainThreadAsync(() => CaissierList.Add(new CaissierData(Caissier.Key, Caissier.Value)));
                }
            }
            IsLoading = false; showData = true;
        }
    }
}
