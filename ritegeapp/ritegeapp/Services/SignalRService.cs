
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;
using RitegeDomain.DTO;
using RitegeDomain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace ritegeapp.Services
{
    public class SignalRService : ISignalRService
    {
       public bool Initialized { get; set; }

        public INotificationService notificationService;

        public HubConnection HubConnection { get; set; }


        public async Task Initialize()
        {
            if (Initialized == false)
            {
                Initialized = true;
                notificationService = DependencyService.Get<INotificationService>();
                HubConnection = new HubConnectionBuilder()
                    .WithUrl(App.HubConnectionURL, options =>
                    {
                        options.HttpMessageHandlerFactory = (message) =>
                        {
                            if (message is HttpClientHandler clientHandler)
                                // always verify the SSL certificate
                                clientHandler.ServerCertificateCustomValidationCallback +=
                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                            return message;
                        };
                        options.AccessTokenProvider = () =>
                        {
                            return Task.FromResult((Application.Current as App).Token);
                        };
                    }).WithAutomaticReconnect()
                    .Build();

                HubConnection.Reconnecting += error =>
                {
                    MessagingCenter.Send(Xamarin.Forms.Application.Current, "Reconnecting");
                    return Task.CompletedTask;
                };
                await Task.Run(async () => { await Connect(); });

                ListenForAlerts();
                StartAlertService();
            }
        }
        public HubConnection GetHub()
        {
            return HubConnection;
        }
        public void StartAlertService()
        {
            DependencyService.Get<IEventService>().StartService();
        }

        public void StopListeningForNotImportantData()
        {
            Debug.WriteLine("dashboardunsub");

            HubConnection.Remove("GetDashboardData");
            HubConnection.Remove("GetTicketData");
        }
        public void ListenForAlerts()
        {
            HubConnection.On<ParkingEvent>("AlertReceived", async (data) =>
            {
                var coderetriever = new XmlErrorCodeStringRetriever();
                var parkingEvent = coderetriever.GetErrorCodeStringAndType(data);
                notificationService.CreateAlertNotification(parkingEvent);
            });
            HubConnection.On<DashBoardDTO>("GetDashboardData", (data) =>
            {
                Debug.WriteLine("dashboard data received");
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetDashboardData", data);

            });
            HubConnection.On<List<InfoTicketDTO>>("GetTicketData", (data) =>
            {
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetTicketData", data);

            });
        }
        public async Task Connect()
        {
            if (HubConnection is null)
            {
                await Task.Run(async () => { await Initialize(); });
                return;
            }
            if ((Application.Current as App).IsOnline)
            {
                if (HubConnection.State == HubConnectionState.Disconnected)
                {
                    try
                    {
                        await HubConnection.StartAsync();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (HubConnection.State == HubConnectionState.Connected)
                {
                }

            }
        }
        public async Task Disconnect()
        {

            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.StopAsync();
            }

        }
    }
}