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
        public List<ParkingEvent> ListDto = new List<ParkingEvent>();

        [ObservableProperty]
        public ObservableCollection<ParkingEvent> listAlert = new ObservableCollection<ParkingEvent>();
        [ObservableProperty]
        public ObservableCollection<ParkingEvent> listAlertToShow = new ObservableCollection<ParkingEvent>();


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
        public DataService dataService;
        #endregion
        public AlertViewerViewModel()
        {
            dataService = (Application.Current as App).dataService;



            dataService.hubConnection.On<ParkingEvent[]>("DangerousAlertReceived", async (data) =>
            {
                await AlertDataReceivedAsync(data.ToList());
            });

        }
        private async Task AlertDataReceivedAsync(List<ParkingEvent> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => ShowNoDataReceivedMessage());
            }
            else
            {if(ListAlertToShow.Count>0)
                    await Device.InvokeOnMainThreadAsync(() => { SetData(data); });
            else
                await Device.InvokeOnMainThreadAsync(() => { UpdateData(data); });
            }
        }
        private void UpdateData(List<ParkingEvent> data)
        {
            var coderetriever = new XmlErrorCodeStringRetriever();


for(int i=0;i<data.Count;i++)
           data[i]= coderetriever.GetErrorCodeStringAndType(data[i]);
            data.ForEach(x => ListAlertToShow.Add(x));
            ShowDataView();
        }
        private void SetData(List<ParkingEvent> data)
        {
            ListAlertToShow.Clear();
            var coderetriever = new XmlErrorCodeStringRetriever();
            for (int i = 0; i < data.Count; i++)
                data[i] = coderetriever.GetErrorCodeStringAndType(data[i]);
            data.ForEach(x => ListAlertToShow.Add(x));
            ShowDataView();
        }

        public void ShowLoading()
        {
            ShowLoadingIndicator = true;
            ShowNoInternetLabel = false;

            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowNoFilterMessage()
        {
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowDataView()
        {
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = true; ShowNoDataReceived = false;
        }
        public void ShowNoInternetView()
        {
            ShowNoInternetLabel = true;
            ShowLoadingIndicator = false;
            ShowData = false; ShowNoDataReceived = false;
        }
        public void ShowNoDataReceivedMessage()
        {
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowData = false; ShowNoDataReceived = true;
        }

        [ICommand]
        private async void ClearFilter(object obj)
        {
            dateStart = DateTime.Now;
            dateEnd = DateTime.Now.AddDays(1);
            await GetData();

        }
        [ICommand]
        public async Task GetData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ShowLoading();
                await AlertDataReceivedAsync(await dataService.GetAlertData(dateStart));
            }
            else
            if (ListDto.Count == 0)
                ShowNoInternetView();
            else
            {
                

            }
        } 
      


      

        

    }
}