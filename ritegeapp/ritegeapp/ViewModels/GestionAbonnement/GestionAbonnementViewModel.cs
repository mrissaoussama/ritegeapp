using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Rg.Plugins.Popup.Services;
    using RitegeDomain.DTO;
using RitegeDomain.Model;
using ritegeapp.Services;
using ritegeapp.Utils;
using ritegeapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using MvvmHelpers;

namespace ritegeapp.ViewModels
{
    public partial class GestionAbonnementViewModel : ObservableObject
    {
        #region variables   
        public List<InfoAbonnementDTO> ListDto = new();

        
        [ObservableProperty]
        public ObservableRangeCollection<GroupAbonnement> listAbonnementToShow = new();

        [ObservableProperty]
        public ObservableRangeCollection<DateAbonnement> listDateAbonnementToShow = new();
        [ObservableProperty]
        private string searchTextBox = "";
        [ObservableProperty]
        private string libelleAbonnement;
        [ObservableProperty]
        private string nomPrenomAbonne;
        [ObservableProperty]
        private Decimal prixAbonnement;
        [ObservableProperty]
        private TypeAbonnementEnum typeAbonnement;

        [ObservableProperty]
        private DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Today.AddMonths(3);

        [ObservableProperty]
        private bool showNoFilterResultLabel = false;

        [ObservableProperty]
        private bool resetFilterButton = false;
        [ObservableProperty]
        private bool expandersAreExpanded;

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
        private bool abonnementSortMode;
        [ObservableProperty]
        private bool dateAbonnementSortMode;
        [ObservableProperty]
        private bool showTotal = false;
        [ObservableProperty]
        private bool canTapFilterImages = false;
        [ObservableProperty]
        private decimal totalMoney;
        #endregion
  
        public GestionAbonnementViewModel()
        {
            AbonnementSortMode = true;
            DateAbonnementSortMode = false;
           

        }
        private async Task FilteredDataReceivedAsync(List<InfoAbonnementDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {
                ListDto = (data);
                ListAbonnementToShow.Clear();
                ListDateAbonnementToShow.Clear();
                UpdateCollections(await GroupByAbonnement(), await GroupByDateAbonnement());
            }
        }

        private void UpdateCollections(List<GroupAbonnement> groupAbonnements, List<DateAbonnement> dateAbonnements)
        {
            ListAbonnementToShow.AddRange(groupAbonnements);
            ListDateAbonnementToShow.AddRange(dateAbonnements);
            CalculateListTotal(ListAbonnementToShow.ToList());
            ShowDataView();
        }

        private async Task<List<GroupAbonnement>> GroupByAbonnement()
        {
            var result = ListDto;
            var Dto = new List<GroupAbonnement>(listAbonnementToShow);
            result = result.GroupBy(p => p.NomPrenomAbonne).Select(grp => grp.FirstOrDefault()).ToList();
            for (int i = 0; i < result.Count; i++)
            { var abonnements = Dto.Where(Abonnement => Abonnement.NomPrenomAbonne == result[i].NomPrenomAbonne).ToList();
                if (abonnements.Count > 0)
                {
                    abonnements[0].ListAbonnement.Add(result[i]);
                }
                else
                {
                    GroupAbonnement c = new GroupAbonnement(ListDto, result[i].NomPrenomAbonne);
                    Dto.Add(c);
                }
            }
            return Dto;
        }

        private void CalculateListTotal(List<GroupAbonnement> dto)
        {
            TotalMoney = dto.Sum(x => x.AbonnementTotal);
        }

        private async Task<List<DateAbonnement>> GroupByDateAbonnement()
        {
            var result = ListDto; 
            var Dto = new List<DateAbonnement>();

            result = result.GroupBy(p => p.DateActivation).Select(grp => grp.FirstOrDefault()).ToList();
            foreach (var x in result)
            { var abonnement = ListDateAbonnementToShow.Where(Abonnement => Abonnement.dateStart == x.DateActivation).ToList();
                if (abonnement.Count > 0)
                {
                    abonnement[0].ListAbonnement.Add(x);
                }
                else
                {
                    DateAbonnement c = new DateAbonnement(ListDto, x.DateActivation);
                    Dto.Add(c);
                }
            }
            return Dto;
        }
        public void ShowLoading()
        {
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
            ShowData = true; 
            ShowNoDataReceived = false;
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
                var list = await (Application.Current as App).dataService.GetAbonnementData(DateStart, DateEnd, SearchTextBox);
                await FilteredDataReceivedAsync(list);
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
        }
        [ICommand]
        private void SortBy(object obj)
        {
            AbonnementSortMode = !AbonnementSortMode;
            DateAbonnementSortMode = !DateAbonnementSortMode;
        }
        [ICommand]
        private async void ClearFilter(object obj)
        {
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today.AddMonths(1);
            SearchTextBox = "";
            ShowNoFilterResultLabel = false;
            await GetData();
        }
        [ICommand]
        private async void OpenStatisticsWindow(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new GestionAbonnementStatisticsPopup(this));
        }
    }
}