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
        public List<InfoAbonnementDTO> ListDto = new List<InfoAbonnementDTO>();

        [ObservableProperty]
        public ObservableRangeCollection<GroupAbonnement> listAbonnement = new ObservableRangeCollection<GroupAbonnement>();
        [ObservableProperty]
        public ObservableRangeCollection<GroupAbonnement> listAbonnementToShow = new ObservableRangeCollection<GroupAbonnement>();
        [ObservableProperty]
        public ObservableRangeCollection<DateAbonnement> listDateAbonnement = new ObservableRangeCollection<DateAbonnement>();
        [ObservableProperty]
        public ObservableRangeCollection<DateAbonnement> listDateAbonnementToShow = new ObservableRangeCollection<DateAbonnement>();
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
        private DateTime dateEnd = DateTime.Today.AddDays(1);

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
        public FilterData filterdata;
        public DataService dataService;

        public GestionAbonnementViewModel()
        {

          
            dataService = (Application.Current as App).dataService;
            AbonnementSortMode = true;
            DateAbonnementSortMode = false;

            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connection Lost", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connexion Perdue"));


            }); 
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connected", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connecté")
            );


            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "No Connection", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Pas De Connexion")
            );

            });


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
            ShowDataView();
        }

        private async Task<List<GroupAbonnement>> GroupByAbonnement()
        {
            var result = ListDto;
            var Dto = new List<GroupAbonnement>(listAbonnementToShow);
            result = result.GroupBy(p => p.NomPrenomAbonne).Select(grp => grp.FirstOrDefault()).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (Dto.Where(Abonnement => Abonnement.NomPrenomAbonne == result[i].NomPrenomAbonne).ToList().Count > 0)
                {
                    Dto.Where(Abonnement => Abonnement.NomPrenomAbonne == result[i].NomPrenomAbonne).ToList()[0].ListAbonnement.Add(result[i]);
                }
                else
                {
                    GroupAbonnement c = new GroupAbonnement(ListDto, result[i].NomPrenomAbonne);

                    Dto.Add(c);
                }
            }
            return Dto;
        }


        private async Task<List<DateAbonnement>> GroupByDateAbonnement()
        {
            var result = ListDto; 
            var Dto = new List<DateAbonnement>();

            result = result.GroupBy(p => p.DateActivation).Select(grp => grp.FirstOrDefault()).ToList();
            foreach (var x in result)
            {
                if (ListDateAbonnementToShow.Where(Abonnement => Abonnement.dateStart == x.DateActivation).ToList().Count > 0)
                {
                    ListDateAbonnementToShow.Where(Abonnement => Abonnement.dateStart == x.DateActivation).ToList()[0].ListAbonnement.Add(x);
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
            ShowTotalIfCountIsMoreThanOne();
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
        private void ShowTotalIfCountIsMoreThanOne()
        {
            if (AbonnementSortMode)
            {
                if (ListAbonnementToShow.Count > 1)
                    ShowTotal = true;
                TotalMoney = ListAbonnementToShow.Sum(x => x.AbonnementTotal);

            }
            else
            if (DateAbonnementSortMode)
            {
                if (ListDateAbonnementToShow.Count > 1)
                    ShowTotal = true;
                TotalMoney = ListDateAbonnementToShow.Sum(x => x.AbonnementTotal);

            }
        }
        [ICommand]
        private async void SearchText(object obj)
        {
            await GetData();
        }
        [ICommand]
        public async Task GetData()
        {
            Debug.WriteLine("clicked");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ShowLoading();
                var list = await dataService.GetFilteredAbonnementData(dateStart, dateEnd, SearchTextBox);
                await FilteredDataReceivedAsync(list);
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
            else
            {
                await Device.InvokeOnMainThreadAsync(() =>
                 MessagingCenter.Send(Xamarin.Forms.Application.Current, "Connection Lost"));

            }

            ListIsRefreshing = false;

        }
        [ICommand]
        private void SortBy(object obj)
        {
            AbonnementSortMode = !AbonnementSortMode;
            DateAbonnementSortMode = !DateAbonnementSortMode;
            ShowTotalIfCountIsMoreThanOne();
        }

        [ICommand]
        private async void ClearFilter(object obj)
        {


            dateStart = DateTime.Now;
            dateEnd = DateTime.Today.AddDays(1);
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