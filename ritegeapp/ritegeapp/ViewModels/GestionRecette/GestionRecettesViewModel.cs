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
        public ObservableCollection<InfoTicketDTO> listRecetteToShow = new ObservableCollection<InfoTicketDTO>();
        [ObservableProperty]
        private bool showNoFilterResultLabel = false;
        [ObservableProperty]
        private bool resetFilterButton;
        [ObservableProperty]
        private Dictionary<int, string> parkingList = new();

        [ObservableProperty]
        private int idParking;
        [ObservableProperty]
        private string parkingName;

 
        [ObservableProperty]
        private DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Today;

        [ObservableProperty]
        private decimal totalMoney;
        [ObservableProperty]
        private ViewStateManager stateManager = new();
        #endregion
        ISignalRService signalRService;
        IDataService dataService;

        public bool Initialized { get; private set; }

        public GestionRecettesViewModel()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            dataService = DependencyService.Get<IDataService>();
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connected", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connecté")
            );
            });
         
            MessagingCenter.Subscribe<Xamarin.Forms.Application, InfoTicketDTO>(Xamarin.Forms.Application.Current, "GetTicketData", async (sender, data) =>
            {
                StateManager.ShowLoading();
                ListDto.Add(data);
                NewDataReceivedAsync(data); CalculateListTotal();
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, ParkingData>(Xamarin.Forms.Application.Current, "TicketViewParkingClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => ParkingChanged(arg.IdParking, arg.ParkingName));
            });
        }

    
        private async Task DataReceivedAsync(List<InfoTicketDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {

                {
                    ListDto = (data);
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        DataReceived(); CalculateListTotal();
                    });
                }
              
            }
        }
     
        private void CalculateListTotal()
        {
            TotalMoney = ListDto.Sum(x => x.MontantPaye);
        }
        private void NewDataReceivedAsync(InfoTicketDTO infoTicketDTOs)
        {
            ListRecetteToShow.Insert(0,infoTicketDTOs);
            StateManager.ShowDataView();
        }
        private void DataReceived()
        {
            ListRecetteToShow.Clear();
            
            ListDto.ForEach(x => ListRecetteToShow.Add(x));
            StateManager.ShowDataView();
        }
        
        [RelayCommand]
        private async void ClearFilter(object obj)
        {
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today;
            
            ShowNoFilterResultLabel = false;
            await GetData();
        }
        [RelayCommand]

        private async void Search(object obj)
        {
         
            await GetData();
        }
        [RelayCommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                StateManager.ShowLoading();
                if (Initialized==false)
                {
                    Initialized = true;
                   StateManager. ParkingIsLoading = true;
                    StateManager.CanClickParkingList = false;
                    ParkingList = await dataService.GetParkingList();
                    StateManager.ParkingIsLoading = false;

                    if (ParkingList.Count != 0)
                    {
                        IdParking = ParkingList.First().Key;
                        ParkingName = ParkingList.First().Value;

                        var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                        await DataReceivedAsync(data);
                        StateManager.CanClickParkingList = true;
                        await Task.Run(async () => { await signalRService.Connect(); });
                        await signalRService.ListenForTicketData(IdParking);

                    }
                }
                else
                {
                    StateManager.CanClickParkingList = false;

                    var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                    await DataReceivedAsync(data);
                    StateManager.CanClickParkingList = true;
                    await Task.Run(async () => { await signalRService.Connect(); });
                }
            }
            else
            if (ListDto.Count == 0)
                StateManager.ShowNoInternetView();
            }
        public async Task ParkingChanged(int idParking, string parkingName)
        {
            if (ParkingName == parkingName)
                Debug.WriteLine("same parking");
            else
            {
                ListDto.Clear();
                StateManager.CanClickParkingList = false;
                IdParking = idParking;
                ParkingName = parkingName;
                StateManager.ShowLoading();
                ListRecetteToShow.Clear();
                var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                await DataReceivedAsync(data);
                StateManager.CanClickParkingList = true;
                await Task.Run(async () => { await signalRService.Connect(); });
                await signalRService.ListenForTicketData(IdParking);

                Debug.WriteLine("different parking");
            }
        }
        [RelayCommand]
        private async void OpenParkingListView(object obj)
        {
            if (StateManager.CanClickParkingList)
                await PopupNavigation.Instance.PushAsync(new ParkingListView(this,"Ticket"));
        }

    }
}