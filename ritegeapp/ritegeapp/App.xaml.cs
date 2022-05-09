using Microsoft.AspNetCore.SignalR.Client;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using ritegeapp.Services;
using ritegeapp.Utils;
using RitegeDomain.Database.Queries.Parking.UtilisateurQueries;
using RitegeDomain.Model;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ritegeapp
{
    public partial class App : Application
    {
       // public const string ServerURL = "https://192.168.1.14:45456";
      // public const string ServerURL = "http://192.168.1.14:45455";
        public const string ServerURL = "https://ritegeserver.conveyor.cloud";
        public const string hubConnectionURL = ServerURL+"/Server";
        public int NotificationID = 3;
        public DataService dataService = new();
        public bool IsOnline;
        public bool IsShowingAlert = false;
        private event EventHandler Starting = delegate { };
        public App()
        {
            InitializeComponent();
            Starting += onStarting;
            Starting(this, EventArgs.Empty);
            MainPage = new AppShell();     
        }
  
        private async void onStarting(object sender, EventArgs args) {
            Starting -= onStarting;

            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            IsOnline = CheckIfConnectedToInternet();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjIzNzIzQDMyMzAyZTMxMmUzMEJwbk0xNVlBUHdJM0I4emZ0K2xMNXViaExUN2Fac1RGaGRQWFFZZEtOZWc9");
            DependencyService.Register<IMessage>();
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connection Lost", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connexion Perdue"));

            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "Connected", async (sender) =>
            {
                _ = Task.Run(async () => await dataService.Connect()); 
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Connecté")
            );
            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "No Connection", async (sender) =>
            {
                await Device.InvokeOnMainThreadAsync(() => DependencyService.Get<IMessage>().LongAlert("Pas De Connexion")
                 );
            });

            await dataService.Initialize();
         
        }
        private async void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            if (e.Request is null)
            {
                return;
            }

        }
        private bool CheckIfConnectedToInternet()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var profiles = Connectivity.ConnectionProfiles;
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                IsOnline = true;
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "Internet Reestablished");
            }
            else
            {
                IsOnline = false;
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "Internet Lost");
            }
        }
        protected  override void OnStart()
        {
            base.OnStart(); //subscribe to event
            Starting += onStarting;
            //raise event
            Starting(this, EventArgs.Empty);
            var hub = DependencyService.Get<IEventService>().GetHub();
            if (hub is not null)
            {
                dataService.hubConnection = DependencyService.Get<IEventService>().GetHub();
            }
            if (CheckIfConnectedToInternet())
            { }
        }

        protected override void OnSleep()
        {
            base.OnSleep(); if (dataService.hubConnection is not null)
            {
               DependencyService.Get<IEventService>().SetHub(dataService.hubConnection);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            var hub = DependencyService.Get<IEventService>().GetHub();
            if (hub is not null)
            {
             dataService.hubConnection=DependencyService.Get<IEventService>().GetHub();
            }
            if (CheckIfConnectedToInternet())
            { }
        }
    }
}
