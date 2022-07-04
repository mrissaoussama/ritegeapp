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
                    await Device.InvokeOnMainThreadAsync(() =>
                    {
                        SetData(data); 
                    });
            }
        }

        private void DoorStateChanged(DoorData doorData)
        {
            StateManager.ShowLoading();
            var index = ListDoor.IndexOf(ListDoor.Where(x => x.IdDoor == doorData.IdDoor).Single());
            doorData.DoorName = ListDoor[index].DoorName;
            ListDoor.Remove(ListDoor.Where(x => x.IdDoor == doorData.IdDoor).Single());
            ListDoor.Insert(index, doorData);
                StateManager.ShowDataView();
        }
        private void SetData(List<DoorData> list)
        {
            ListDoor = new(list);
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
            if (Initialized == false)
            {
                Initialized = true;
                StateManager.ParkingIsLoading = true;
                StateManager.CanClickParkingList = false;
                ParkingList = await dataService.GetParkingList();
                StateManager.ParkingIsLoading = false;

                if (ParkingList.Count != 0)
                {
                    IdParking = ParkingList.First().Key;
                    ParkingName = ParkingList.First().Value;
                    await DataReceivedAsync(await dataService.GetDoorList(IdParking));
                    StateManager.CanClickParkingList = true;
                    await Task.Run(async () => { await signalRService.Connect(); });
                    await signalRService.ListenForDoorData(IdParking);

                }
            }
            else
            {
                StateManager.ShowLoading();
                await DataReceivedAsync(await dataService.GetDoorList(IdParking));
                StateManager.ShowDataView();
            }
        }
        public async Task ParkingChanged(int idParking, string parkingName)
        {
            if (ParkingName == parkingName)
                Debug.WriteLine("same parking");
            else
            {
                StateManager.CanClickParkingList = false;
                IdParking = idParking;
                ParkingName = parkingName;
                StateManager.ShowLoading();
                ListDoor.Clear();
                await DataReceivedAsync(await dataService.GetDoorList(IdParking));
                StateManager.CanClickParkingList = true;
                await Task.Run(async () => { await signalRService.Connect(); });
                await signalRService.ListenForDoorData(IdParking);

                Debug.WriteLine("different parking");
            }
        }
        [RelayCommand]
        private async void OpenParkingListView(object obj)
        {
            if (StateManager.CanClickParkingList)
                await PopupNavigation.Instance.PushAsync(new ParkingListView(this,"Door"));
        }
        [RelayCommand]
        private async void DoorCheckBoxClicked(object obj)
        {
            var doordata = obj as DoorData;
            if(doordata != null)
            await dataService.ChangeDoorState(doordata.IdDoor,doordata.IdController,doordata.DoorState);
        }
        public async Task PageLeft()
        {
            Initialized = false;
        await    signalRService.StopListeningForDoorData();
        }

    }
}