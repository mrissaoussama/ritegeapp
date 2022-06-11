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
        [ObservableProperty]
        private bool showData;
        [ObservableProperty]
        private bool showNoDataReceived;
        [ObservableProperty]
        private bool showLoadingIndicator;
        [ObservableProperty]
        private bool showNoInternetLabel;
        [ObservableProperty]
        private bool showNoFilterResultLabel;
        [ObservableProperty]
        private bool listIsRefreshing = false;
        public XmlErrorCodeStringRetriever codeRetriever = new XmlErrorCodeStringRetriever();
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
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {
                    await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
            }
        }
        public async Task UpdateData(EventDTO data)
        {               data = codeRetriever.GetErrorCodeString(data);

            ListEventToShow.Add(data);
            ShowDataView();
        }
        private void SetData(List<EventDTO> data)
        {
            ListEventToShow.Clear();
            for (int i = 0; i < data.Count; i++)
                data[i] = codeRetriever.GetErrorCodeString(data[i]);
            data.ForEach(x => ListEventToShow.Add(x));
            ShowDataView();
        }
        public void ShowLoading()
        {
            ShowNoFilterResultLabel = false;
            ShowLoadingIndicator = true;
            ShowNoInternetLabel = false;
            ShowData = false;
            ShowNoDataReceived = false;
        }
        public void ShowNoFilterMessage()
        {
            ShowNoFilterResultLabel = true;
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = false;
            ShowNoDataReceived = false;
        }
        public void ShowDataView()
        {
            ShowNoFilterResultLabel = false;

            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = true;
            ShowNoDataReceived = false;
        }
        public void ShowNoInternetView()
        {
            ShowNoFilterResultLabel = false;

            ShowNoInternetLabel = true;
            ShowLoadingIndicator = false;
            ShowData = false;
            ShowNoDataReceived = false;
        }
        public void ShowNoDataReceivedMessage()
        {
            ShowNoFilterResultLabel = false;

            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = false;
            ShowNoDataReceived = true;
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
                if(Initialized==false)
                {
                    Initialized = true;
                    await signalRService.ListenForEventData();
                }
                ShowLoading();
           await EventDataReceivedAsync(await dataService.GetEventData(DateStart,DateEnd.AddDays(1).AddTicks(-1)));
                await signalRService.Connect();
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
        }
    }
}