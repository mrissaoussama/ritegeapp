using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Rg.Plugins.Popup.Services;
    using RitegeDomain.DTO;
using ritegeapp.Services;
using ritegeapp.Utils;
using ritegeapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using RitegeDomain.Model;

namespace ritegeapp.ViewModels
{
    public partial class GestionDesSessionsCaissiersViewModel : ObservableObject
    {
        #region variables 
        public List<InfoSessionsDTO> ListDto = new List<InfoSessionsDTO>();
        [ObservableProperty]
        public ObservableCollection<Caissier> listCaissierToShow = new ObservableCollection<Caissier>();
        [ObservableProperty]
        public ObservableCollection<DateCaissier> listDateCaissierToShow = new ObservableCollection<DateCaissier>();

        [ObservableProperty]
        private string caissier;
        [ObservableProperty]
        private string parking,nomCaissier;
        [ObservableProperty]
        private int idCaissier;
        [ObservableProperty]
        public Dictionary<int, string> listCaissier;
        [ObservableProperty]
        private DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Today;
        [ObservableProperty]
        private bool showNoFilterResultLabel = false;
        [ObservableProperty]
        private bool resetFilterButton = false;
        [ObservableProperty]
        private bool caissierSortMode;
        [ObservableProperty]
        private bool dateCaissierSortMode;
        [ObservableProperty]
        private ViewStateManager stateManager = new();
        [ObservableProperty]
        private decimal totalMoney;
        #endregion      
        IDataService dataService;

        public bool Initialized { get; private set; }

        public GestionDesSessionsCaissiersViewModel()
        {
            dataService = DependencyService.Get<IDataService>();

            CaissierSortMode = true;
            DateCaissierSortMode = false;
            MessagingCenter.Subscribe<Xamarin.Forms.Application, CaissierData>(Xamarin.Forms.Application.Current, "CaissierClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => CaissierClicked(arg.IdCaisser, arg.CaissierName));
            });
        }
        private async Task DataReceivedAsync(List<InfoSessionsDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {
                ListDto = (data); GroupByCaissier(); GroupByDateCaissier();
                CalculateListTotal(data);
            }
        }
        private void GroupByCaissier()
        {
            var result = ListDto;
            result = result.GroupBy(p => p.Caissier).Select(grp => grp.FirstOrDefault()).ToList();
            ListCaissierToShow.Clear();
            foreach (var x in result)
            { var caissier = ListCaissierToShow.Where(caissier => caissier.NomCaissier == x.Caissier).ToList();
                if (caissier.Count > 0)
                {
                    caissier[0].ListSessions.Add(x);
                }
                else
                {
                    Caissier c = new Caissier(ListDto, x.Caissier);
                    ListCaissierToShow.Add(c);
                }
            }
            StateManager.ShowDataView();
        }
        private void GroupByDateCaissier()
        {
            var result = ListDto;
            result = result.GroupBy(p => p.DateStartSession).Select(grp => grp.FirstOrDefault()).ToList();
            ListDateCaissierToShow.Clear();
            foreach (var x in result)
            { 
                var caissier = ListDateCaissierToShow.Where(caissier => caissier.Date == x.DateStartSession).ToList();
                if (caissier.Count > 0)
                {
                    caissier[0].ListSessions.Add(x);
                }
                else
                {
                    DateCaissier c = new DateCaissier(ListDto, x.DateStartSession);
                    ListDateCaissierToShow.Add(c);
                }
            }

        }
      
        private void CalculateListTotal(List<InfoSessionsDTO> dto)
        {
            TotalMoney = dto.Sum(x => x.Recette);
        }
        [ICommand]
        private async void ClearFilter(object obj)
        {
            dateStart = DateTime.Today;
            dateEnd = DateTime.Today;

            ShowNoFilterResultLabel = false;
            await GetData();
        }
        [ICommand]
        private async void SearchText(object obj)
        {
            await GetData();
        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                StateManager.ShowLoading();
                if(Initialized==false)
                {
                    Initialized = true;
                    ListCaissier = await dataService.GetCashierList();
                    Caissier = "—";
                }

                await DataReceivedAsync(await dataService.GetCashierData(dateStart, dateEnd.AddDays(1).AddTicks(-1), IdCaissier));
              

            }
            else
            if (ListDto.Count == 0)
                StateManager.ShowNoInternetView();
        }
        public async Task CaissierClicked(int idCashRegister, string CaissierName)
        {
            if (Caissier == CaissierName)
                Debug.WriteLine("same caisse");
            else
            {
                IdCaissier = idCashRegister;
                Caissier = CaissierName;
                
                await DataReceivedAsync(await dataService.GetCashierData(dateStart, dateEnd.AddDays(1).AddTicks(-1), IdCaissier));
                StateManager.ShowDataView();
                Debug.WriteLine("different caisse");
            }
        }
        [ICommand]
        private void SortBy(object obj)
        {
            CaissierSortMode = !CaissierSortMode;
            DateCaissierSortMode = !DateCaissierSortMode;  
        }
        [ICommand]
        private async void OpenStatisticsWindow(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new GestionCaissierStatisticsPopup(this));
        }
        
        [ICommand]
        private async void OpenCaissierList(object obj)
        {
            if (StateManager.CanClickCaissierList)
                await PopupNavigation.Instance.PushAsync(new CaissierListView(this));
        }

    }
}