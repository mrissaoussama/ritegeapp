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
    public partial class DoorViewModel : ObservableObject
    {
        #region variables
        [ObservableProperty]
        public ObservableCollection<DoorData> listDoor = new ObservableCollection<DoorData>();
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
        private ViewStateManager stateManager = new();
        #endregion
        ISignalRService signalRService;
        IDataService dataService;

        public bool Initialized { get; private set; }

        public DoorViewModel()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            dataService = DependencyService.Get<IDataService>();

    
            MessagingCenter.Subscribe<Xamarin.Forms.Application, DoorData>(Xamarin.Forms.Application.Current, "DoorStateChanged", async (sender, data) =>
            {
                StateManager.ShowLoading();
                DoorStateChanged(data); 
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application, ParkingData>(Xamarin.Forms.Application.Current, "DoorViewParkingClicked", async (sender, arg) =>
            {
                await Device.InvokeOnMainThreadAsync(() => ParkingChanged(arg.IdParking, arg.ParkingName));
            });
        }

    
        private async Task DataReceivedAsync(List<DoorData> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {

                {
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        DataReceived(data); 
                    });
                }
              
            }
        }

        private void DoorStateChanged(DoorData DoorDatas)
        {
            ListDoor.Insert(0,DoorDatas);
            StateManager.ShowDataView();
        }
        private void DataReceived(List<DoorData> list)
        {
            ListDoor = new(list);
            StateManager.ShowDataView();
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
            var door1 = new DoorData { DoorName = "Porte_1", DoorState = true };
            var door2 = new DoorData { DoorName = "Porte_2", DoorState = true };
            var door3 = new DoorData { DoorName = "Porte_3", DoorState = false };
            var door4 = new DoorData { DoorName = "Porte_4", DoorState = true };
            var door5 = new DoorData { DoorName = "Porte_5", DoorState = false };
            var door6 = new DoorData { DoorName = "Porte_6", DoorState = true };
            var door7 = new DoorData { DoorName = "Porte_7", DoorState = true };
            var door8 = new DoorData { DoorName = "Porte_8", DoorState = false };
            var door9 = new DoorData { DoorName = "Porte_9", DoorState = false };
            ListDoor.Add(door1);
            ListDoor.Add(door2);
            ListDoor.Add(door3);
            ListDoor.Add(door4);
            ListDoor.Add(door5);
            ListDoor.Add(door6);
            ListDoor.Add(door7);
            ListDoor.Add(door8);
            ListDoor.Add(door9);
            StateManager.ShowDataView();
            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    StateManager.ShowLoading();
            //    if (Initialized==false)
            //    {
            //        Initialized = true;
            //       StateManager. ParkingIsLoading = true;
            //        StateManager.CanClickParkingList = false;
            //        ParkingList = await dataService.GetParkingList();
            //        StateManager.ParkingIsLoading = false;

            //        if (ParkingList.Count != 0)
            //        {
            //            IdParking = ParkingList.First().Key;
            //            ParkingName = ParkingList.First().Value;

            //            var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
            //            await DataReceivedAsync(data);
            //            StateManager.CanClickParkingList = true;
            //            await Task.Run(async () => { await signalRService.Connect(); });
            //            await signalRService.ListenForTicketData(IdParking);

            //        }
            //    }
            //    else
            //    {
            //        StateManager.CanClickParkingList = false;

            //        var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
            //        await DataReceivedAsync(data);
            //        StateManager.CanClickParkingList = true;
            //        await Task.Run(async () => { await signalRService.Connect(); });
            //    }
            //}
            //else
            //if (ListDto.Count == 0)
            //    StateManager.ShowNoInternetView();
        }
        public async Task ParkingChanged(int idParking, string parkingName)
        {
            //if (ParkingName == parkingName)
            //    Debug.WriteLine("same parking");
            //else
            //{
            //    ListDto.Clear();
            //    StateManager.CanClickParkingList = false;
            //    IdParking = idParking;
            //    ParkingName = parkingName;
            //    StateManager.ShowLoading();
            //    ListRecetteToShow.Clear();
            //    var data = await dataService.GetTicketData(DateStart, DateEnd.AddDays(1).AddTicks(-1), IdParking);
            //    await DataReceivedAsync(data);
            //    StateManager.CanClickParkingList = true;
            //    await Task.Run(async () => { await signalRService.Connect(); });
            //    await signalRService.ListenForTicketData(IdParking);

            //    Debug.WriteLine("different parking");
         //   }
        }
        [ICommand]
        private async void OpenParkingListView(object obj)
        {
            if (StateManager.CanClickParkingList)
                await PopupNavigation.Instance.PushAsync(new ParkingListView(this,"Door"));
        }

    }
}