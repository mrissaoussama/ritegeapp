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
  
        private XmlEventCodeStringRetriever codeRetriever=new XmlEventCodeStringRetriever();
        #endregion
        ISignalRService signalRService;
        IDataService dataService;
        [ObservableProperty]
        private ViewStateManager stateManager = new();
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
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {
                    await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
          
            }
        }
        public async Task UpdateData(EventDTO data)
        {
            data = codeRetriever.GetErrorCodeString(data);

            ListAlertToShow.Insert(0, data);
            StateManager.ShowDataView();
        }
        private void SetData(List<EventDTO> data)
        {
            ListAlertToShow.Clear();
            for (int i = 0; i < data.Count; i++)
                data[i] = codeRetriever.GetErrorCodeString(data[i]);
            data.ForEach(x => ListAlertToShow.Add(x));
            StateManager.ShowDataView();
        }
     
        [RelayCommand]
        private async void ClearFilter(object obj)
        {
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now.AddDays(1).AddTicks(-1);
            await GetData();
        }
        [RelayCommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (Initialized==false)
                {
                    Initialized = true;
                    await signalRService.ListenForAlerts();
                }
                StateManager.ShowLoading();
                await OnAlertDataReceivedAsync(await dataService.GetAlertData(DateStart, DateEnd.AddDays(1).AddTicks(-1)));
                await signalRService.Connect();
            }
            else
            if (ListDto.Count == 0)
                StateManager.ShowNoInternetView();
        } 
    }
}