using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ritegeapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableauDeBord : ContentPage
    {
        private bool IsStarted;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (IsStarted == false)
            {
                IsStarted =true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                await Task.Run(() => ((TableauDeBordViewModel)BindingContext).GetData());

#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            //            Task.Run(async () =>
            //);

        }
        public TableauDeBord()
        {
            InitializeComponent();
            BindingContext = new TableauDeBordViewModel();
            KeyPressed += CustomPage_KeyPressed;

        }
        //void SetProgress(CircularProgressBar view, double value)
        //{

        //    if (value > 100)
        //        value = 0;
        // //   SpotsUsed.Text = value.ToString();
        //    view.Progress = value;
        //}
        //void AddProgress(CircularProgressBar view, double add)
        //{
        //    var value = view.Progress + add;

        //    if (value > 100)
        //        value = 0;

        //    view.Progress = value;
        //}
        public event EventHandler<KeyEventArgs> KeyPressed;

        public void SendKeyPressed(object sender, KeyEventArgs e)
        {
            KeyPressed?.Invoke(sender, e);
        }


        private void CustomPage_KeyPressed(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Key pressed: " + e.Key);
        }


        public class KeyEventArgs : EventArgs
        {
            public string Key { get; set; }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


    }
}
