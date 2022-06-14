using ritegeapp.Services;
using ritegeapp.ViewModels;
using ritegeapp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ritegeapp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GestionDesSessionsCaissiers), typeof(GestionDesSessionsCaissiers));
            Routing.RegisterRoute(nameof(TableauDeBord), typeof(TableauDeBord));


            if (string.IsNullOrEmpty(DependencyService.Get<IDataService>().GetToken().Result))
            {
                     CurrentItem = login;
            }
            else
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjIzNzIzQDMyMzAyZTMxMmUzMEJwbk0xNVlBUHdJM0I4emZ0K2xMNXViaExUN2Fac1RGaGRQWFFZZEtOZWc9");
                 DependencyService.Get<IDataService>().Initialize();
                DependencyService.Get<ISignalRService>().Initialize();
                DependencyService.Get<ISignalRService>().StartAlertService();
                
                  CurrentItem = dashboard;

            }
            MessagingCenter.Subscribe<Xamarin.Forms.Application, string>(Xamarin.Forms.Application.Current, "CommunicationError", async (sender, arg) =>
             {
                 Device.BeginInvokeOnMainThread(async () =>
                       {
                         if ((Application.Current as App).IsShowingAlert == false)
                         {
                               (Application.Current as App).IsShowingAlert = true;
                               await DisplayAlert("Alert","Erreur de communication au serveur", "OK");
                               (Application.Current as App).IsShowingAlert = false;
                         }
                     });
             });
            MessagingCenter.Subscribe<Xamarin.Forms.Application >(Xamarin.Forms.Application.Current, "SwitchToLoginView", async (sender) =>
            {
                 CurrentItem = login;

            });
            MessagingCenter.Subscribe<Xamarin.Forms.Application>(Xamarin.Forms.Application.Current, "SwitchToDashboardView", async (sender) =>
            {
              CurrentItem = dashboard;

                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjIzNzIzQDMyMzAyZTMxMmUzMEJwbk0xNVlBUHdJM0I4emZ0K2xMNXViaExUN2Fac1RGaGRQWFFZZEtOZWc9");
                await DependencyService.Get<IDataService>().Initialize();
                await DependencyService.Get<ISignalRService>().Initialize();
            });
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IDataService>().Disconnect();
             CurrentItem = login;

        }
    }
}
