using ritegeapp.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ritegeapp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoorView : ContentPage
    {
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((DoorViewModel)BindingContext).GetData());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            //            Task.Run(async () =>
            //);

        }
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((DoorViewModel)BindingContext).PageLeft());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            //            Task.Run(async () =>
            //);

        }
        public DoorView()
        {
            InitializeComponent();
            BindingContext = new DoorViewModel();

            //var items = new List<ToolbarItemModel>
            //{
            //    new ToolbarItemModel {ImagePath = "collapse.png", MenuText = "First Item"},
            //    new ToolbarItemModel {ImagePath = "expand.png", MenuText = "Second Item"}
            //};
            //SecondaryToolbarListView.ItemsSource = items;
        }
        //private void ToolbarItem_Clicked(object sender, EventArgs e)
        //{
        //    SecondaryToolbarListView.IsVisible = !SecondaryToolbarListView.IsVisible;
        //    SecondaryToolbarListView.SelectedItem = null;
        //}

        //private void SecondaryToolbarListView_Unfocused(object sender, FocusEventArgs e)
        //{
        //    SecondaryToolbarListView.IsVisible = !SecondaryToolbarListView.IsVisible;
        //    SecondaryToolbarListView.SelectedItem = null;

        //}
        //private void SecondaryToolbarListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    SecondaryToolbarListView.SelectedItem = null;
        //    SecondaryToolbarListView.IsVisible = !SecondaryToolbarListView.IsVisible;

        //}
     
        private async void Filter_Clicked(object sender, EventArgs e)
        {
            //       Navigation.ShowPopup(new popup01());
        }

        private void Expand1_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            if (((View)sender).GetType() == typeof(Frame))
            {
                Expander exp = (Expander)((Frame)sender).GetChildren()[0];
                exp.IsExpanded = !exp.IsExpanded;
            }


        }
    }
}
