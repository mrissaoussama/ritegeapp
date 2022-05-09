using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Services;
using RitegeDomain.Model;
using ritegeapp.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using RitegeDomain.Database.Entities.Parking;

namespace ritegeapp.ViewModels
{
    public class GestionAbonnementStatisticsPopupViewModel : ExtendedBindableObject
    {
        public GestionAbonnementStatisticsPopupViewModel(ObservableObject viewmodel)
        {
            parentvm = viewmodel;
            ListeAbonnements = new ObservableCollection<GroupAbonnement>(((GestionAbonnementViewModel)parentvm).ListAbonnementToShow);
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

            NbAbonnementTotal += ListeAbonnements.Count;
            PrixAbonnementTotal += ListeAbonnements.Sum(x => x.AbonnementTotal);
            ListeAbonnements.ForEach(x =>
            {

                AnnuelTotal += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Annuel);
                IntervalTotal += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Interval);
                SemestrielTotal += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Semestriel);

            });
        }

        #region variables

        private decimal _prixAbonnementTotal;
        public decimal PrixAbonnementTotal
        {
            get { return _prixAbonnementTotal; }
            set { _prixAbonnementTotal = value; RaisePropertyChanged(() => PrixAbonnementTotal); }
        }
        private int _nbAbonnementTotal;
        public int NbAbonnementTotal
        {
            get { return _nbAbonnementTotal; }
            set { _nbAbonnementTotal = value; RaisePropertyChanged(() => NbAbonnementTotal); }
        }
        private int _intervalTotal;
        public int IntervalTotal
        {
            get { return _intervalTotal; }
            set { _intervalTotal = value; RaisePropertyChanged(() => IntervalTotal); }
        }
        private int _abonnelTotal;
        public int AnnuelTotal
        {
            get { return _abonnelTotal; }
            set { _abonnelTotal = value; RaisePropertyChanged(() => AnnuelTotal); }
        }
        private int _semestrielTotal;
        public int SemestrielTotal
        {
            get { return _semestrielTotal; }
            set { _semestrielTotal = value; RaisePropertyChanged(() => SemestrielTotal); }
        }

        private ObservableCollection<GroupAbonnement> _listeAbonnements;
        public ObservableCollection<GroupAbonnement> ListeAbonnements
        {
            get { return _listeAbonnements; }
            set { _listeAbonnements = value; RaisePropertyChanged(() => ListeAbonnements); }
        }

        #endregion
    }
}
