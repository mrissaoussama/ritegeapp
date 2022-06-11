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
    public partial class AlertViewerViewModel : ObservableObject
    {
        #region variables      
        public List<EventDTO> ListDto = new List<EventDTO>();
        [ObservableProperty]
        public ObservableCollection<EventDTO> listAlertToShow = new ObservableCollection<EventDTO>();
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
        private XmlErrorCodeStringRetriever codeRetriever=new XmlErrorCodeStringRetriever();
        #endregion
        ISignalRService signalRService;
        IDataService dataService;

        public bool Initialized { get; private set; }

        public AlertViewerViewModel()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            dataService=DependencyService.Get<IDataService>();
            MessagingCenter.Subscribe<Xamarin.Forms.Application, EventDTO>(Xamarin.Forms.Application.Current, "GetAlertData", async (sender, data) =>
            {
                await UpdateData(data);

            });
        }
        private async Task OnAlertDataReceivedAsync(List<EventDTO> data)
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
        {
            data = codeRetriever.GetErrorCodeString(data);

            ListAlertToShow.Add(data);
            ShowDataView();
        }
        private void SetData(List<EventDTO> data)
        {
            ListAlertToShow.Clear();
            for (int i = 0; i < data.Count; i++)
                data[i] = codeRetriever.GetErrorCodeString(data[i]);
            data.ForEach(x => ListAlertToShow.Add(x));
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
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now.AddDays(1).AddTicks(-1);
            await GetData();
        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (Initialized==false)
                {
                    Initialized = true;
                    await signalRService.ListenForAlerts();
                }
                ShowLoading();
                await OnAlertDataReceivedAsync(await dataService.GetAlertData(DateStart, DateEnd.AddDays(1).AddTicks(-1)));
                await signalRService.Connect();
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
        } 
    }
}