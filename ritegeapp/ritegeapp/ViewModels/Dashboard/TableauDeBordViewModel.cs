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
        private string parking,nomPrenomCaissier,caissier,caisse;
        [ObservableProperty]
        private bool etatCaisse,isLoading,eventListLoading,isRefreshing,showData,canClickParkingOrCashRegister;
        [ObservableProperty]
        private int? placeDisponible,placeMax,placeOccupe,nbTickets,nbAdministrateur,nbAutorite,nbEgress,nbAbonne,nbEvents;
        [ObservableProperty]
        Decimal? recetteParking, recetteCaissier, recetteCaisse, fluxBorneTotal, fluxCaisseTotal;
        public List<ParkingEvent> eventList = new();
        #endregion

        IDataService dataService;

        public TableauDeBordViewModel()
        {
            dataService = DependencyService.Get<IDataService>();

            SubscribeToEvents();
            InitData();
        }
        public void SubscribeToEvents()
        {
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Internet Reestablished", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connexion Retablie"));
                await GetData();
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "No Connection", async (sender) =>
            {
                SetDataToNull();
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "ParkingClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => ParkingChanged((string)arg));
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "CashRegisterClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => CashRegisterClicked((string)arg));
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, DashBoardDTO>(Xamarin.Forms.Application.Current, "GetDashboardData", async (sender, arg) =>
            {
                await OnDataReceivedAsync(arg);
            });
        }
        private void SetDataToNull()
        {
            EtatCaisse = false;
            FluxBorne = null;
            FluxCaisse = null;
            RecetteCaisse = null;
            RecetteCaissier = null;
            RecetteParking = null;
            NomPrenomCaissier = null;
            Parking = null;
            Caisse = null;
            NbAdministrateur = null;
            NbAutorite = null;
            NbEgress = null;
            NbTickets = null;
            NbAbonne = null;
            PlaceMax = null;
            PlaceDisponible = null;
            PlaceOccupe = null;
            FluxBorneTotal = null;
            FluxCaisseTotal = null;

            CanClickParkingOrCashRegister = false;
        }
        private void InitData()
        {
            EtatCaisse = true;
            FluxBorne = Flux.Entree;
            FluxCaisse = Flux.Entree;
            RecetteCaisse = 99999;
            RecetteCaissier = 99999;
            RecetteParking = 99999;
            NomPrenomCaissier = "-------";
            Parking = "-------";
            Caisse = "-------";
            EtatCaisse = true;
            NbAdministrateur = 99999;
            NbAutorite = 99999;
            NbEgress = 99999;
            NbTickets = 99999;
            NbAbonne = 99999;
            PlaceMax = null;
            this.PlaceDisponible = null;
            this.PlaceOccupe = null;
            FluxBorneTotal = 99999;
            FluxCaisseTotal = 99999;
            CanClickParkingOrCashRegister = false;

        }
        private async Task OnDataReceivedAsync(DashBoardDTO data)
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
          
                FluxBorneTotal = 10;
                FluxCaisseTotal = 15;
            
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
            CanClickParkingOrCashRegister = false;

            IsLoading = true; IsRefreshing = true; ShowData = false;
        }
        public void ShowDataView()
        {
            CanClickParkingOrCashRegister = true;

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
                var data = await dataService.GetDashboardData(1);
                await OnDataReceivedAsync(data);
                if (data is null)
                {

                    SetDataToNull();
                }
                //else 
               // await GetEventList();
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
            if(CanClickParkingOrCashRegister)
            await PopupNavigation.Instance.PushAsync(new ParkingListView(this));
        }
        [ICommand]
        private async void OpenCashRegisterListView(object obj)
        {
            if (CanClickParkingOrCashRegister)
                await PopupNavigation.Instance.PushAsync(new CashRegisterListView(this));
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
        public void CashRegisterClicked(string caisse)
        {
            if (Caisse == caisse)
                Debug.WriteLine("same caisse");
            else
            {
                Caisse = caisse;
                Debug.WriteLine("different caisse");
            }
        }
        public async Task GetEventList()
        {
            var list = (await dataService.GetLast10Events());
            if (list !=null && (list.Count > 0))
            {
                eventList = list;
                NbEvents = eventList.Count;
            }
        }
    }
}