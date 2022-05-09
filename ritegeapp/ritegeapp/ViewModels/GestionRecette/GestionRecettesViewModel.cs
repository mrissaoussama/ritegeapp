﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class GestionRecettesViewModel : ObservableObject
    {
        #region variables      
        public List<InfoTicketDTO> ListDto = new List<InfoTicketDTO>();

        [ObservableProperty]
        public ObservableCollection<InfoTicketDTO> listRecette = new ObservableCollection<InfoTicketDTO>();
        [ObservableProperty]
        public ObservableCollection<InfoTicketDTO> listRecetteToShow = new ObservableCollection<InfoTicketDTO>();

        [ObservableProperty]

        private DateTime dateHeureEntree;
        [ObservableProperty]

        private bool showNoFilterResultLabel = false;
        [ObservableProperty]

        private bool resetFilterButton = false;
        [ObservableProperty]

        private string statisticsToolBarItemName = "Statistiques";
        [ObservableProperty]

        private string expandOption1Text = "Développer Dates";
        [ObservableProperty]
        private DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Today.AddDays(1);
        private ImageSource expandOption1Image = ImageSource.FromFile("expand.png");
        [ObservableProperty]
        private bool showData;
        [ObservableProperty]
        private bool showNoDataReceived;
        [ObservableProperty]
        private bool showLoadingIndicator;
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
        public FilterData filterdata;

        public DataService dataService;

        public GestionRecettesViewModel()
        {
            dataService = (Application.Current as App).dataService;

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

            dataService.hubConnection.On<InfoTicketDTO[]>("GetFilteredTicketData", async (data) =>
            {
               await FilteredDataReceivedAsync(data.ToList());
            });

        }
        private async Task FilteredDataReceivedAsync(List<InfoTicketDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {
                ListDto = (data);
                await Device.InvokeOnMainThreadAsync(() => { GroupByDate(); });
            }
        }

        private void GroupByDate()
        {


            ListRecetteToShow.Clear();
            ListDto.ForEach(x => ListRecetteToShow.Add(x));
            ShowDataView();


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
            {
                if (ListRecetteToShow.Count > 1)
                    ShowTotal = true;
                TotalMoney = ListRecetteToShow.Sum(x => x.MontantPaye);
            }


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
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
              
                ShowLoading();
                var data = await (Application.Current as App).dataService.GetFilteredTicketData(dateStart.ToUniversalTime(), dateEnd.ToUniversalTime());

                await FilteredDataReceivedAsync(data);

                 dataService.Connect();

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

        private bool CanExpandOption1Command(object arg)
        {
            return CanCheckStatistics(arg);
        }




        private bool CanCheckStatistics(object arg)
        {
            return ShowNoFilterResultLabel == false || ListRecetteToShow.Count > 0;
        }
        private bool CanResetFilter(object arg)
        {
            return ResetFilterButton;
        }
        [ICommand]
        private async void OpenStatisticsWindow(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new GestionRecetteStatisticsPopup(this));
        }





    }
}