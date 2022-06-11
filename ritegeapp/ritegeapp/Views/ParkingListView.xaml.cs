using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;

namespace ritegeapp.Views
{
    public partial class ParkingListView : PopupPage
    {
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((ParkingListViewViewModel)BindingContext).LoadList());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            //            Task.Run(async () =>
            //);

        }

        public ParkingListView(ObservableObject viewmodel,string viewName)
        {
            InitializeComponent();
            BindingContext = new ParkingListViewViewModel(viewmodel,viewName);
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
