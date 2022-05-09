using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ritegeapp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionDesSessionsCaissiers : ContentPage
    {
        Expander selectedExpander;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((GestionDesSessionsCaissiersViewModel)BindingContext).GetData());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            //            Task.Run(async () =>
            //);

        }
        public GestionDesSessionsCaissiers()
        {
            InitializeComponent();
            BindingContext = new GestionDesSessionsCaissiersViewModel();
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
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void Filter_Clicked(object sender, EventArgs e)
        {
            //       Navigation.ShowPopup(new popup01());
        }


        private void Expand1_Clicked(object sender, EventArgs e)
        {
            if (selectedExpander == null)
            {
                selectedExpander = ((Expander)sender);

            }
            else
            {
                if (!((Expander)sender).Equals(selectedExpander))
                {
                    selectedExpander.IsExpanded = false;

                    selectedExpander = ((Expander)sender);

                }

            }

        }
        async void OnImageTapped(object sender, EventArgs e)
        {

        }

        private async void ContentPage_Unfocused(object sender, FocusEventArgs e)
        {
            // await ((GestionDesSessionsCaissiersViewModel)BindingContext).Disconnect();

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
