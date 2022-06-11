using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ritegeapp.Views
{
    public partial class LoginView : ContentPage
    {
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            //     Task.Run(() => ((EventListPopupViewModel)BindingContext).LoadList());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            //            Task.Run(async () =>
            //);
            Shell.SetTabBarIsVisible(this, false);
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
    }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Shell.SetTabBarIsVisible(this, true);
            Shell.SetNavBarIsVisible(this, true); Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);

        }
        public LoginView()
        {
            var vm = new LoginVM();
            InitializeComponent();
            BindingContext = vm;
            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.GetDataCommand.Execute(null);
            };
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
