using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ritegeapp.ViewModels;
using System;
using Xamarin.Forms;

namespace ritegeapp.Views
{
    public partial class GestionAbonnementStatisticsPopup : PopupPage
    {
        public GestionAbonnementStatisticsPopup(IGestionAbonnementViewModel viewmodel)
        {
            InitializeComponent();
            BindingContext = DependencyService.Get<IGestionAbonnementViewModel>();
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
