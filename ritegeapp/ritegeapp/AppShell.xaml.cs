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
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
