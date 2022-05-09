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
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class TableauDeBordViewModel : ObservableObject
    {
        #region variables
        [ObservableProperty]
        private Flux? fluxBorne, fluxCaisse;
        [ObservableProperty]

        private string parking;
        [ObservableProperty]

        private bool? etatCaisse;
        [ObservableProperty]
        private bool isLoading;
        [ObservableProperty]
        private bool eventListLoading;
        [ObservableProperty]
        private bool isRefreshing;
        [ObservableProperty]
        private bool showData;
        [ObservableProperty]
        private int? placeDisponible;
        [ObservableProperty]
        private int? placeMax;
        [ObservableProperty]
        private int? placeOccupe;
        [ObservableProperty]
        private int? nbTickets;
        [ObservableProperty]
        private int? nbAdministrateur;
        [ObservableProperty]
        private int? nbAutorite;
        [ObservableProperty]
        private int? nbEgress;
        [ObservableProperty]
        private int? nbAbonne;
        [ObservableProperty]
        private int? nbEvents;
        [ObservableProperty]
        private string nomPrenomCaissier;
        [ObservableProperty]
        private string caissier;
        [ObservableProperty]

        private string caisse;
        [ObservableProperty]

        private DateTime todayDate;
        [ObservableProperty]

        Decimal? recetteParking, recetteCaissier, recetteCaisse, fluxBorneTotal, fluxCaisseTotal;
        public List<ParkingEvent> eventList = new();
        #endregion


        public TableauDeBordViewModel()
        {

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TodayDate = DateTime.Now;
                return true;
            });
        
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Internet Reestablished", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connexion Retablie"));
                await GetData();


            });

            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "No Connection", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Pas De Connexion"));
                SetDataToNull();
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "ParkingClicked", async (sender, arg) =>
              {
                  await Device.InvokeOnMainThreadAsync(() => ParkingChanged((string)arg));
              });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, DashBoardDTO>(Xamarin.Forms.Application.Current, "GetDashboardData", async (sender, arg) =>
            {
                await DataReceivedAsync(arg);
            });
       
            InitData();

        }
        private void SetDataToNull()
        {
            EtatCaisse = null;
            FluxBorne = null;
            FluxCaisse = null;
            this.RecetteCaisse = null;
            this.RecetteCaissier = null;
            this.RecetteParking = null;
            this.NomPrenomCaissier = null;
            this.Parking = null;
            this.Caisse = null;
            this.EtatCaisse = null;
            this.NbAdministrateur = null;
            this.NbAutorite = null;
            this.NbEgress = null;
            this.NbTickets = null;
            this.NbAbonne = null;
            PlaceMax = null;
            this.PlaceDisponible = null;
            this.PlaceOccupe = null;
            FluxBorneTotal = null;
            FluxCaisseTotal = null;
            
        }
        private void InitData()
        {
            EtatCaisse = true;
            FluxBorne = Flux.Entree;
            FluxCaisse = Flux.Entree;
            this.RecetteCaisse = 99999;
            this.RecetteCaissier = 99999;
            this.RecetteParking = 99999;
            this.NomPrenomCaissier = "-------";
            this.Parking = "-------";
            this.Caisse = "-------";
            this.EtatCaisse = true;
            this.NbAdministrateur = 99999;
            this.NbAutorite = 99999;
            this.NbEgress = 99999;
            this.NbTickets = 99999;
            this.NbAbonne = 99999;
            PlaceMax = null;
            this.PlaceDisponible = null;
            this.PlaceOccupe = null;
            FluxBorneTotal = 99999;
            FluxCaisseTotal = 99999;
            
        }

        private async Task DataReceivedAsync(DashBoardDTO data)
        {
            if (data == null)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {
                await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
            }
        }
        private void SetData(DashBoardDTO data)
        {
            if (FluxBorneTotal is null)
            {
                FluxBorneTotal = 10;
                FluxCaisseTotal = 15;
            }
            Parking = data.Parking;
            FluxBorneTotal += 10;
            FluxCaisseTotal += 15;
            EtatCaisse = data.EtatCaisse;
            FluxBorne = data.FluxBorne;
            FluxCaisse = data.FluxCaisse;
            this.RecetteCaisse = data.RecetteCaisse;
            this.RecetteCaissier = data.RecetteCaissier;
            this.RecetteParking = data.RecetteParking;
            this.NomPrenomCaissier = data.NomPrenomCaissier;
            this.Parking = data.Parking;
            this.Caisse = data.Caisse;
            this.EtatCaisse = data.EtatCaisse;
            this.NbAdministrateur = data.NbAdministrateur;
            this.NbAutorite = data.NbAutorite;
            this.NbEgress = data.NbEgress;
            this.NbTickets = data.NbTickets;
            this.NbAbonne = data.NbAbonne; PlaceMax = data.PlaceMax;
            this.PlaceDisponible = data.PlaceDisponible;
            this.PlaceOccupe = PlaceMax - data.PlaceDisponible; 
            ShowDataView();

        }
        public void ShowLoading()
        {
            IsLoading = true; IsRefreshing = true; ShowData = false;
        }
        public void ShowDataView()
        {
            IsLoading = false; IsRefreshing = false; ShowData = true;


        }
        public void ShowNoDataReceivedMessage()
        {
        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ShowLoading();
                var data = await (Application.Current as App).dataService.GetDashboardData(1);
                await DataReceivedAsync(data);
                if (data is null)
                {
                    SetDataToNull();
                }

                else await GetEventList();
            }
            else

            {
                SetDataToNull();

            }
            ShowDataView();
        }
        [ICommand]
        private async void OpenParkingListView(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new ParkingListView(this));
        }
        [ICommand]

        private async void OpenEventListView(object obj)
        {if(eventList!=null || eventList.Count!=0)
            await PopupNavigation.Instance.PushAsync(new EventListView(this));
        }
        public  void ParkingChanged(string parking)
        {
            if (Parking == parking)
                Debug.WriteLine("same parking");
            else
            {
                Parking = parking;
                Debug.WriteLine("different parking");
            }
        }
        public async Task GetEventList()
        {
            var list = (await (Application.Current as App).dataService.GetLast10Events());
            if (list !=null && (list.Count > 0))
            {
                eventList = list;
                NbEvents = eventList.Count;
            }
        }



    }
}