using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ritegeapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionAbonnement : ContentPage
    {
        Expander selectedExpander;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((GestionAbonnementViewModel)BindingContext).GetData());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        }
        public GestionAbonnement()
        {
            InitializeComponent();
            BindingContext = new GestionAbonnementViewModel();



        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void Filter_Clicked(object sender, EventArgs e)
        {
            //       Navigation.ShowPopup(new popup01());
        }
        async void OnImageTapped(object sender, EventArgs e)
        {
            await ((Image)sender).ScaleTo(0.8, 100);

            await ((Image)sender).ScaleTo(1, 100);
        }
        private void Expand1_Clicked(object sender, EventArgs e)
        {
            if (selectedExpander == null)
            {
                ((Expander)sender).ForceUpdateSize();
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

        private void Expand21_Clicked(object sender, FocusEventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            //if (((View)sender).GetType() == typeof(Frame))
            //{
            //    Expander exp = (Expander)((Frame)sender).GetChildren()[0];

            //    Debug.WriteLine("clicked on Frame");
            //    Debug.WriteLine(exp.IsExpanded);

            //    exp.IsExpanded = !exp.IsExpanded;
            //    exp.ForceUpdateSize();
            //    Debug.WriteLine(exp.IsExpanded);

            //}


        }
    }
}
