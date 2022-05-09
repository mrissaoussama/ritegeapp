using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ritegeapp.ViewModels;
using System;

namespace ritegeapp.Views
{
    public partial class GestionRecetteStatisticsPopup : PopupPage
    {
        public GestionRecetteStatisticsPopup(ObservableObject viewmodel)
        {
            InitializeComponent();
            BindingContext = new GestionRecetteStatisticsPopupViewModel(viewmodel);
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
