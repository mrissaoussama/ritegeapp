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
    public partial class EventViewerViewModel : ObservableObject
    {
        #region variables      
        public List<EventDTO> ListDto = new List<EventDTO>();
        [ObservableProperty]
        public ObservableCollection<EventDTO> listEventToShow = new ObservableCollection<EventDTO>();
        [ObservableProperty]
        private DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        private DateTime dateEnd = DateTime.Today;
      
        public XmlEventCodeStringRetriever codeRetriever = new XmlEventCodeStringRetriever();
        [ObservableProperty]
        private ViewStateManager stateManager = new();
        #endregion        
        ISignalRService signalRService;
        IDataService dataService;

        public bool Initialized { get; private set; }

        public EventViewerViewModel()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            dataService = DependencyService.Get<IDataService>();

            MessagingCenter.Subscribe<Xamarin.Forms.Application, EventDTO>(Xamarin.Forms.Application.Current, "GetEventData", async (sender, data) =>
            {
                await UpdateData(data);

            });
        }

        private async Task EventDataReceivedAsync(List<EventDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {
                    await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
            }
        }
        public async Task UpdateData(EventDTO data)
        {               data = codeRetriever.GetErrorCodeString(data);

            ListEventToShow.Insert(0,data);
            StateManager.ShowDataView();
        }
        private void SetData(List<EventDTO> data)
        {
            ListEventToShow.Clear();
            for (int i = 0; i < data.Count; i++)
                data[i] = codeRetriever.GetErrorCodeString(data[i]);
            data.ForEach(x => ListEventToShow.Add(x));
            StateManager.ShowDataView();
        }
   
        [ICommand]
        private async void ClearFilter(object obj)
        {
            DateStart = DateTime.Today;
            DateEnd = DateTime.Today.AddDays(1).AddTicks(-1);
            await GetData();
        }
        [ICommand]
        private async void EventExpanded(object obj)
        {
            Debug.WriteLine(obj);
        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                StateManager.ShowLoading();

                if(Initialized==false)
                {
                    Initialized = true;
                    
                    await Task.Run(async () => { await signalRService.ListenForEventData(); });

                }
                await EventDataReceivedAsync(await dataService.GetEventData(DateStart,DateEnd.AddDays(1).AddTicks(-1)));
                await signalRService.Connect();
            }
            else
            if (ListDto.Count == 0)
                StateManager.ShowNoInternetView();
        }
    }
}