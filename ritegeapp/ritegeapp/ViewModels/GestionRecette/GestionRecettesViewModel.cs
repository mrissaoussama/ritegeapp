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
        private bool resetFilterButton, canClickParkingList,parkingIsLoading;
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
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {

                {
                    ListDto = (data);
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        GroupByDate(); CalculateListTotal();
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
            ListRecetteToShow.Add(infoTicketDTOs);
            ShowDataView();
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
            ShowData = true;
            ShowTotal = true;
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
        private void ShowTotalIfCountIsMoreThanOne()
        {
           if(ListRecetteToShow.Count > 1)
              ShowTotal = true;
              TotalMoney = ListRecetteToShow.Sum(x => x.MontantPaye);
        }
        [ICommand]
        private async void ClearFilter(object obj)
        {
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today;
            
            ShowNoFilterResultLabel = false;
            await GetData();
        }
        [ICommand]

        private async void Search(object obj)
        {
         
            await GetData();
        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
             ShowLoading();
                if (Initialized==false)
                {
                    Initialized = true;
                    ParkingIsLoading = true;
                    CanClickParkingList = false;
                    ParkingList = await dataService.GetParkingList();
                    ParkingIsLoading = false;

                    if (ParkingList.Count != 0)
                    {
                        IdParking = ParkingList.First().Key;
                        ParkingName = ParkingList.First().Value;

                        var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                        await DataReceivedAsync(data);
                        CanClickParkingList = true;
                        await Task.Run(async () => { await signalRService.Connect(); });
                        await signalRService.ListenForTicketData(IdParking);

                    }
                }
                else
                {
                    CanClickParkingList = false;

                    var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                    await DataReceivedAsync(data);
                    CanClickParkingList = true;
                    await Task.Run(async () => { await signalRService.Connect(); });
                }
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
            }
        public async Task ParkingChanged(int idParking, string parkingName)
        {
            if (ParkingName == parkingName)
                Debug.WriteLine("same parking");
            else
            {
                ListDto.Clear();
                CanClickParkingList = false;
                IdParking = idParking;
                ParkingName = parkingName;
                ShowLoading();
                ListRecetteToShow.Clear();
                var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
                await DataReceivedAsync(data);
                CanClickParkingList = true;
                await Task.Run(async () => { await signalRService.Connect(); });
                await signalRService.ListenForTicketData(IdParking);

                Debug.WriteLine("different parking");
            }
        }
        [ICommand]
        private async void OpenParkingListView(object obj)
        {
            if (CanClickParkingList)
                await PopupNavigation.Instance.PushAsync(new ParkingListView(this,"Ticket"));
        }

    }
}