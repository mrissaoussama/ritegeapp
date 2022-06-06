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
        private string searchTextBox = "";
        [ObservableProperty]
        private string caissier;
        [ObservableProperty]
        private string parking;
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
        private bool showLoadingIndicator;
        [ObservableProperty]
        private bool showData;
        [ObservableProperty]
        private bool showNoDataReceived;
        [ObservableProperty]
        private bool showNoInternetLabel;
        [ObservableProperty]
        private bool listIsRefreshing = false;
        [ObservableProperty]
        private bool showTotal = false;
        [ObservableProperty]
        private bool canTapFilterImages = false;
        [ObservableProperty]
        private decimal totalMoney;
        #endregion      
        IDataService dataService;

        public GestionDesSessionsCaissiersViewModel()
        {
            dataService = DependencyService.Get<IDataService>();

            CaissierSortMode = true;
            DateCaissierSortMode = false;
           
        }
        private async Task FilteredDataReceivedAsync(List<InfoSessionsDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
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
            ShowDataView();
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
        public void ShowLoading()
        {
            CanTapFilterImages = false;
            ShowTotal = false;
            ShowLoadingIndicator = true;
            ShowNoFilterResultLabel = false;
            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowNoFilterMessage()
        {
            ShowNoInternetLabel = false;
            ShowTotal = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = true;
            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowDataView()
        {
            CanTapFilterImages = true;
            ShowTotal = true;
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = true; ShowNoDataReceived = false;
        }
        public void ShowNoInternetView()
        {
            ShowNoInternetLabel = true;
            ShowTotal = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowNoDataReceivedMessage()
        {
            ShowTotal = false;
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = false; ShowNoDataReceived = true;
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
            SearchTextBox = "";
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
                ShowLoading();
                await FilteredDataReceivedAsync(await dataService.GetCashierData(dateStart, dateEnd, SearchTextBox));
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
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
    }
}