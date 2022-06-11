using Microsoft.AspNetCore.SignalR.Client;

using Plugin.LocalNotification.EventArgs;
using ritegeapp.Services;
using ritegeapp.Utils;
using ritegeapp.ViewModels;
using ritegeapp.Views;
using System;

using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ritegeapp
{
    public partial class App : Application
    {
         //public const string ServerURL = "https://192.168.1.14:45458";
        // public const string ServerURL = "http://192.168.1.14:45455";
        public const string ServerURL = "https://ritegeserver.conveyor.cloud";
        public const string HubConnectionURL = ServerURL+"/Server";

        public bool IsOnline; public bool IsShowingAlert = false;

        public string? Token { get; set; }
        private event EventHandler Starting = delegate { };
        public App()
        {
            RegisterDependencies();
            InitializeComponent();
            IsOnline = CheckIfConnectedToInternet();
            Connectivity.ConnectivityChanged += ConnectivityChanged;



           // MainPage = new NavigationPage(new LoginView());
            MainPage = new AppShell(); 
         

        }
        private async void onStarting(object sender, EventArgs args) {
        
        
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjIzNzIzQDMyMzAyZTMxMmUzMEJwbk0xNVlBUHdJM0I4emZ0K2xMNXViaExUN2Fac1RGaGRQWFFZZEtOZWc9");
            await DependencyService.Get<IDataService>().Initialize();
            await DependencyService.Get<ISignalRService>().Initialize();

        }
        void RegisterDependencies()
        {
            DependencyService.Register<IGestionAbonnementViewModel, GestionAbonnementViewModel>();
         DependencyService.RegisterSingleton<ISignalRService>(new SignalRService());
           DependencyService.RegisterSingleton<ISignalRService>(new SignalRService());

      DependencyService.Register<INotificationService, NotificationService>();
            DependencyService.RegisterSingleton<IDataService>(new DataService());

        }
        private bool CheckIfConnectedToInternet()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
        
        void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
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
            //raise event
            Starting(this, EventArgs.Empty);
            var hub = DependencyService.Get<IEventService>().GetHub();
            if (hub is not null)
            {
                var signalRService = DependencyService.Get<ISignalRService>();

                signalRService.HubConnection = DependencyService.Get<IEventService>().GetHub();
            }
            if (CheckIfConnectedToInternet())
            { }
        }

        protected override void OnSleep()
        {
            var signalRService = DependencyService.Get<ISignalRService>();
            base.OnSleep();
            if (signalRService.Initialized)
            {
               DependencyService.Get<IEventService>().SetHub(signalRService.HubConnection);
                signalRService.StopListeningForDashboardData();
                signalRService.StopListeningForTicketData();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            var hub = DependencyService.Get<IEventService>().GetHub();
            if (hub is not null)
            {
                var signalRService = DependencyService.Get<ISignalRService>();

                signalRService.HubConnection = DependencyService.Get<IEventService>().GetHub();
                signalRService.ListenForAlerts();
            }
            if (CheckIfConnectedToInternet())
            { }
        }
    }
}
