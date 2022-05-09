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
        public ObservableCollection<Caissier> listCaissier = new ObservableCollection<Caissier>();
        [ObservableProperty]
        public ObservableCollection<Caissier> listCaissierToShow = new ObservableCollection<Caissier>();
        [ObservableProperty]
        public ObservableCollection<DateCaissier> listDateCaissier = new ObservableCollection<DateCaissier>();
        [ObservableProperty]
        public ObservableCollection<DateCaissier> listDateCaissierToShow = new ObservableCollection<DateCaissier>();
        [ObservableProperty]
        private string searchTextBox = "";
        [ObservableProperty]
        private string caissier;
        [ObservableProperty]
        private string parking;
        [ObservableProperty]
        private DateTime dateStartSession;
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

        [ObservableProperty]
        private string statisticsToolBarItemName = "Statistiques";


        [ObservableProperty]
        private string expandOption1Text = "Développer Dates";


        [ObservableProperty]
        private ImageSource expandOption1Image = ImageSource.FromFile("expand.png");


        #endregion
        public FilterData filterdata;
        public DataService dataService;
        public GestionDesSessionsCaissiersViewModel()
        {
            dataService = (Application.Current as App).dataService;
            CaissierSortMode = true;
            DateCaissierSortMode = false;

            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connection Lost", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connexion Perdue"));


            }); MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connected", async (sender) =>
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

        private async Task FilteredDataReceivedAsync(List<InfoSessionsDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {
                ListDto = (data); GroupByCaissier(); GroupByDateCaissier();
            }
        }

        private void GroupByCaissier()
        {
            var result = ListDto;
            result = result.GroupBy(p => p.Caissier).Select(grp => grp.FirstOrDefault()).ToList();
            ListCaissierToShow.Clear();
            foreach (var x in result)
            {
                if (ListCaissierToShow.Where(caissier => caissier.NomCaissier == x.Caissier).ToList().Count > 0)
                {
                    ListCaissierToShow.Where(caissier => caissier.NomCaissier == x.Caissier).ToList()[0].ListSessions.Add(x);
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
                if (ListDateCaissierToShow.Where(caissier => caissier.Date == x.DateStartSession).ToList().Count > 0)
                {
                    ListDateCaissierToShow.Where(caissier => caissier.Date == x.DateStartSession).ToList()[0].ListSessions.Add(x);
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
            if (CaissierSortMode)
            {
                if (ListCaissierToShow.Count > 1)
                    ShowTotal = true;
                TotalMoney = ListCaissierToShow.Sum(x => x.AbonneTotal);
            }
            else
            if (DateCaissierSortMode)
            {
                if (ListDateCaissierToShow.Count > 1)
                    ShowTotal = true;
                TotalMoney = ListDateCaissierToShow.Sum(x => x.AbonneTotal);

            }

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

        private void UpdatePreferencesUI()
        {
            if (expandersAreExpanded)
            {
                expandOption1Text = "Reduire Dates";
                expandOption1Image = ImageSource.FromFile("collapse.png");
            }
            else
            {
                expandOption1Text = "Developper Dates";
                expandOption1Image = ImageSource.FromFile("expand.png");
            }
        }
        private void LoadPreferences()
        {
            if (Preferences.ContainsKey("ExpandCaissierDates"))
            {
                expandersAreExpanded = Preferences.Get("ExpandCaissierDates", false);
            }
            else
            {
                expandersAreExpanded = false;

                Preferences.Set("ExpandCaissierDates", expandersAreExpanded);
            }
            UpdatePreferencesUI();
        }
        private void SwitchViewPreferences()
        {
            Debug.WriteLine("value before switch" + expandersAreExpanded);

            expandersAreExpanded = !expandersAreExpanded;
            if (Preferences.ContainsKey("ExpandCaissierDates"))
            {
                Preferences.Set("ExpandCaissierDates", expandersAreExpanded);
            }
            UpdatePreferencesUI();

            Debug.WriteLine("value after switch" + expandersAreExpanded);
            Debug.WriteLine("pref after switch" + Preferences.Get("ExpandCaissierDates", false));
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
        
                    await FilteredDataReceivedAsync(await dataService.GetFilteredCashierData(dateStart, dateEnd, SearchTextBox));

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
            CaissierSortMode = !CaissierSortMode;
            DateCaissierSortMode = !DateCaissierSortMode; ShowTotalIfCountIsMoreThanOne();

        }
        [ICommand]
        private async void OpenStatisticsWindow(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new GestionCaissierStatisticsPopup(this));
        }


        [ICommand]
        private void ExpandOption1(object obj)
        {
            SwitchViewPreferences();


            UpdatePreferencesUI();
        }



    }
}