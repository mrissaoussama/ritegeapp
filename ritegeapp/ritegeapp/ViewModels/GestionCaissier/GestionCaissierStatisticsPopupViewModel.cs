using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Services;
using RitegeDomain.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public partial class GestionCaissierStatisticsPopupViewModel : ObservableObject
    {
        public GestionCaissierStatisticsPopupViewModel(ObservableObject viewmodel)
        {
            parentvm = viewmodel;
            ListeAbonnements = (((GestionDesSessionsCaissiersViewModel)parentvm).ListCaissierToShow);
            // SelectedCaissier = ListeAbonnements.First().ToString();
            CalculateStatistics();
        }
        public ObservableObject parentvm;
        public ICommand BackgroundClickedCommand => new Command(BackgroundClickedCommandExecute);

        private async void BackgroundClickedCommandExecute(object parameter)
        {
            await PopupNavigation.Instance.PopAllAsync();

        }
        public void CalculateStatistics()
        {
            foreach (var info in ListeAbonnements)
            {
                AbonneTotal += info.AbonneTotal;
                AdministratifTotal += info.AdministratifTotal;
                AutoriteTotal += info.AutoriteTotal;
                RecetteTotal += info.RecetteTotal;
                TicketTotal += info.TicketTotal;
            }
        }

        #region variables
        [ObservableProperty]
        private int ticketTotal;
        [ObservableProperty]
        private int autoriteTotal;
        [ObservableProperty]
        private int administratifTotal;
        [ObservableProperty]
        private int abonneTotal;
        [ObservableProperty]
        private decimal recetteTotal;
        [ObservableProperty]
        private ObservableCollection<Caissier> listeAbonnements;

        #endregion
    }
}
