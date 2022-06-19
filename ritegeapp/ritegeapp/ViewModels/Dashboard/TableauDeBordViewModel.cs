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
using System.Linq;
using System.Runtime.CompilerServices;

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
        private bool etatCaisse,initialized;
        [ObservableProperty]
        private int? placeDisponible,placeMax,placeOccupe,nbTickets,nbAdministrateur,nbAutorite,nbEgress,nbAbonne,nbEvents,fluxBorneTotal, fluxCaisseTotal;
        [ObservableProperty]
        private int idParking, idCaisse;
        [ObservableProperty]
        Decimal? recetteParking, recetteCaissier, recetteCaisse;
        [ObservableProperty]
        private Dictionary<int, string> parkingList, cashRegisterList;
        public List<ParkingEvent> eventList = new();
        [ObservableProperty]
        private ViewStateManager stateManager = new();
        #endregion

        IDataService dataService;
        ISignalRService signalRService;

        public TableauDeBordViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
            signalRService = DependencyService.Get<ISignalRService>();
            ParkingList = new();
            CashRegisterList = new();
            SubscribeToEvents();
            // InitData();
            //Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            //{
            //    // Do something
            //    PlaceOccupe += 5;
            //    PlaceDisponible -= 5;
            //    if(PlaceDisponible<-1)
            //    {
            //        PlaceDisponible = 100;
            //        PlaceOccupe = 0;
            //    }
            //    if (PlaceOccupe > 100)
            //    {PlaceOccupe=100;
            //        PlaceOccupe = 0;
            //    }
            //    return true; // True = Repeat again, False = Stop the timer
            //});
        }
        public void SubscribeToEvents()
        {
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Internet Reestablished", async (sender) =>
            {
                await GetData();
            });

            MessagingCenter.Subscribe<Xamarin.Forms.Application, ParkingData>(Xamarin.Forms.Application.Current, "ParkingClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => ParkingChanged(arg.IdParking,arg.ParkingName));
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, CashRegisterData>(Xamarin.Forms.Application.Current, "CashRegisterClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => CashRegisterClicked(arg.CashRegisterId,arg.CashRegisterName));
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, DashBoardDTO>(Xamarin.Forms.Application.Current, "GetDashboardData", async (sender, arg) =>
            {
                 UpdateData(arg);
            });
        }

        private void UpdateData(DashBoardDTO data)
        {
          if(!string.IsNullOrEmpty(data.Caisse))
            if (IdCaisse == int.Parse(data.Caisse))
            {
                
                this.RecetteCaisse += data.RecetteCaisse;
                this.RecetteCaissier += data.RecetteCaissier;
                this.NbAdministrateur += data.NbAdministrateur;
                this.NbAutorite += data.NbAutorite;
                this.NbEgress += data.NbEgress;
                this.NbTickets += data.NbTickets;
                this.NbAbonne += data.NbAbonne;
            }

            this.RecetteParking += data.RecetteParking;
      
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

            StateManager.CanClickParkingOrCashRegister = false;
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
            StateManager.CanClickParkingOrCashRegister = false;

        }
        private async Task OnDataReceivedAsync(DashBoardDTO data)
        {
            if (data == null)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
                SetDataToNull();
            }
            else
            {
                await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
            }
        }
        private void SetData(DashBoardDTO data)
        {
 
            FluxBorneTotal=12;
            FluxCaisseTotal = 15;
            FluxBorne = data.FluxBorne;
            FluxCaisse = data.FluxCaisse;
            if(data.Caisse is not null)
            if (IdCaisse == int.Parse(data.Caisse))
            {            RecetteCaissier = data.RecetteCaissier;

            }             
            RecetteCaisse = data.RecetteCaisse;

          RecetteParking = data.RecetteParking;
            NomPrenomCaissier = data.NomPrenomCaissier;
            EtatCaisse = true;
            NbAdministrateur = data.NbAdministrateur;
            NbAutorite = data.NbAutorite;
           NbEgress = data.NbEgress;
           NbTickets = data.NbTickets;
            NbAbonne = data.NbAbonne; 
     
            PlaceMax = data.PlaceMax;
            this.PlaceDisponible = data.PlaceDisponible;
            this.PlaceOccupe = PlaceMax - data.PlaceDisponible;
            StateManager.ShowDataView();

        }
      
        [RelayCommand]
        private async void GetDataButton(object obj)
        {
            await GetData(); StateManager.IsRefreshing = false;
        }
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (initialized == false)
                {
                    StateManager.ShowLoading();

                    initialized = true;
                    StateManager.ParkingIsLoading = true;
                    //await Task.Run(async () => {  ParkingList= await dataService.GetParkingList(); });
                    ParkingList = await dataService.GetParkingList();
                    StateManager.ParkingIsLoading = false;
                    if(ParkingList is not null)
                    if (ParkingList.Count != 0)
                    {
                        IdParking = ParkingList.First().Key;
                        Parking = ParkingList.First().Value;
                            StateManager.CashRegisterIsLoading = true;
                        CashRegisterList = await dataService.GetCashRegisterList(IdParking);
                            //CashRegisterList = dataService.GetCashRegisterList(IdParking).Result;
                            //await Task.Run(async () => { CashRegisterList = await dataService.GetCashRegisterList(IdParking); });

                            StateManager.CashRegisterIsLoading = false;
                        if (CashRegisterList.Count != 0)
                        {
                            Caisse = CashRegisterList.First().Value;
                            IdCaisse = CashRegisterList.First().Key;
                                await Task.Run(async () => {
                                    await signalRService.ListenForDashboardData(IdParking, IdCaisse);
                                });

                            var data = await dataService.GetDashboardData(IdParking, IdCaisse);
                            await OnDataReceivedAsync(data);
                            if (data is null)
                            {

                                SetDataToNull();
                            }
                        }
                        else { Caisse = null; }
                    }
                    else { Parking = null; }
                 
                    // await Task.Run(async () => { await signalRService.ListenForDashboardData(IdParking, IdCaisse);                    });

                }
                else
                {
                    DashBoardDTO data=new();
                    if(IdCaisse!=0)
                     data = await dataService.GetDashboardData(IdParking, IdCaisse);
                    await OnDataReceivedAsync(data);
                    if (data is null)
                    {

                        SetDataToNull();
                    }
                }
            
                //else 
               // await GetEventList();
            }
            else

            {
                SetDataToNull();
            }
            StateManager.ShowDataView();
        }
        [RelayCommand]

        private async void OpenParkingListView(object obj)
        {
            if(StateManager.CanClickParkingOrCashRegister)
            await PopupNavigation.Instance.PushAsync(new ParkingListView(this,"Dashboard"));
        }
        [RelayCommand]
        private async void OpenCashRegisterListView(object obj)
        {
            if (StateManager.CanClickParkingOrCashRegister)
                await PopupNavigation.Instance.PushAsync(new CashRegisterListView(this));
        }

        public  async Task ParkingChanged(int idParking,string nameParking)
        {
            if (Parking == nameParking)
                Debug.WriteLine("same parking");
            else
            {
                IdParking = idParking;
                Parking = nameParking;
                StateManager.ShowLoading();
                await signalRService.ListenForDashboardData(idParking, null);
                await Task.Run(async () => { CashRegisterList = await dataService.GetCashRegisterList(IdParking); });

                if (CashRegisterList.Count != 0)
                {
                    Caisse = CashRegisterList.First().Value;
                    IdCaisse = CashRegisterList.First().Key;
                    await OnDataReceivedAsync(await dataService.GetDashboardData(IdParking, IdCaisse));

                }
                StateManager.ShowDataView();
                Debug.WriteLine("different parking");
            }
        }
        
        public async Task CashRegisterClicked(int idCashRegister,string nomCashRegister)
        {
            if (Caisse == nomCashRegister)
                Debug.WriteLine("same caisse");
            else
            {
                IdCaisse = idCashRegister;
                Caisse = nomCashRegister;
                StateManager.ShowLoading();

                await signalRService.ListenForDashboardData(null, idCashRegister);
                    await OnDataReceivedAsync(await dataService.GetDashboardData(IdParking, IdCaisse));
                StateManager.ShowDataView();

                Debug.WriteLine("different caisse");
            }
        }
        //[RelayCommand]
        //private async void OpenEventListView(object obj)
        //{
        //    if (eventList != null || eventList.Count != 0)
        //        await PopupNavigation.Instance.PushAsync(new EventListView(this));
        //}
        //public async Task GetEventList()
        //{
        //    var list = (await dataService.GetLast10Events());
        //    if (list !=null && (list.Count > 0))
        //    {
        //        eventList = list;
        //        NbEvents = eventList.Count;
        //    }
        //}
    }
}