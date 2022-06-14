
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using RitegeDomain.Database.Queries.ParkingDBQueries.UtilisateurQueries;
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
        private bool isListeningToDashboardData;
        private bool isListeningToTicketData;
        private bool isListeningToAlerts;
        private bool isListeningToEvents;

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
             //   HubConnection.ServerTimeout = TimeSpan.FromSeconds(3);
                HubConnection.Reconnecting += error =>
                {
                    MessagingCenter.Send(Xamarin.Forms.Application.Current, "Reconnecting");
                    return Task.CompletedTask;
                };
                await Connect();
                ListenForAlerts();
                //    StartAlertService();
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

        public async Task ListenForTicketData(int idParking)
        {
            await Initialize(); if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("SetTicketParking", idParking);
                if (isListeningToTicketData == false)
                {
                    isListeningToTicketData = true;

                    HubConnection.On<InfoTicketDTO>("GetTicketData", (data) =>
                    {
                        MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetTicketData", data);
                    });
                }
            }
        }
        public async Task StopListeningForTicketData()
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("SetTicketParking", -1);
                HubConnection.Remove("GetTicketData");
            }
            isListeningToTicketData = false;
        }
        public async Task ListenForDashboardData(int? idparking, int? idcaisse)
        {
            await Initialize(); 
            if (HubConnection.State == HubConnectionState.Connected)
            {
                if (idparking is not null)
                await HubConnection.InvokeAsync("SetDashboardParking", idparking);
            if (idcaisse is not null)
                await HubConnection.InvokeAsync("SetCashRegister", idcaisse);
                if (isListeningToDashboardData == false)
                {
                    isListeningToDashboardData = true;
                    HubConnection.On<DashBoardDTO>("GetDashboardData", (data) =>
                    {
                        MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetDashboardData", data);
                    });
                }
            }
        }
        public async Task StopListeningForDashboardData()
        {if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("SetDashboardParking", -1);
                await HubConnection.InvokeAsync("SetCashRegister", -1);
                HubConnection.Remove("GetDashboardData");
            }                isListeningToDashboardData = false;

        }
        public async Task ListenForAlerts()
        {
            await Initialize();
            if (HubConnection.State == HubConnectionState.Connected)
            {  if (isListeningToAlerts == false)
                {
                    isListeningToAlerts = true;
                    HubConnection.On<EventDTO>("GetAlertData", async (data) =>
                    {
                        MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetAlertData", data);

                        var coderetriever = new XmlEventCodeStringRetriever();
                        var parkingEvent = coderetriever.GetErrorCodeString(data);
                        notificationService.CreateAlertNotification(parkingEvent);
                    });
                      }
              }
        

        }
        public async Task ListenForEventData()
        {
            await Initialize();
            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("SetIsListeningToEvents", true);
                if (isListeningToEvents == false)
                {
                    isListeningToEvents = true;

                    HubConnection.On<EventDTO>("GetEventData", (data) =>
                    {
                        MessagingCenter.Send(Xamarin.Forms.Application.Current, "GetEventData", data);
                    });

                }
            }
        }
        public async Task StopListeningForEventData()
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.InvokeAsync("SetIsListeningToEvents", false);
                HubConnection.Remove("GetEventData");
            }
            isListeningToEvents = false;
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