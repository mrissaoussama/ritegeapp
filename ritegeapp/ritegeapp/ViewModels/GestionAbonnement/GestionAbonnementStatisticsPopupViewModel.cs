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
    public partial class GestionAbonnementStatisticsPopupViewModel : ObservableObject
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
            
            AbonnementCount += ListeAbonnements.Sum(x=>x.AbonnementCount);
            AbonnementTotal += ListeAbonnements.Sum(x => x.AbonnementTotal);
            ListeAbonnements.ForEach(x =>
            {

                AnnuelCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Annuel);
                HebdomadaireCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Hebdomadaire);
                JourCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Jour);
                IntervalleCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Interval);
                MensuelCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Mensuel);
                SemestrielCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Semestriel);
                TrimestrielCount += x.ListAbonnement.Count(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Trimestriel);


                AnnuelTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Annuel).Sum(filtered=>filtered.PrixAbonnement);
                HebdomadaireTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Hebdomadaire).Sum(filtered => filtered.PrixAbonnement);
                JourTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Jour).Sum(filtered => filtered.PrixAbonnement);
                IntervalleTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Interval).Sum(filtered => filtered.PrixAbonnement);
                MensuelTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Mensuel).Sum(filtered => filtered.PrixAbonnement);
                SemestrielTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Semestriel).Sum(filtered => filtered.PrixAbonnement);
                TrimestrielTotal += x.ListAbonnement.Where(y => y.TypeAbonnement == RitegeDomain.DTO.TypeAbonnementEnum.Trimestriel).Sum(filtered => filtered.PrixAbonnement);

            });
        }

        #region variables
        [ObservableProperty]
        private decimal abonnementTotal;
        [ObservableProperty]

        private int abonnementCount;
 

        [ObservableProperty]

        private decimal annuelTotal;
        [ObservableProperty]

        private int annuelCount;

        [ObservableProperty]

        private decimal hebdomadaireTotal;
        [ObservableProperty]

        private int hebdomadaireCount;
        [ObservableProperty]

        private decimal intervalleTotal;
        [ObservableProperty]

        private int intervalleCount;

        [ObservableProperty]

        private decimal jourTotal;
        [ObservableProperty]

        private int jourCount;
        [ObservableProperty]

        private decimal mensuelTotal;
        [ObservableProperty]

        private int mensuelCount;


        [ObservableProperty]

        private decimal semestrielTotal;
        [ObservableProperty]

        private int semestrielCount;

        [ObservableProperty]

        private decimal trimestrielTotal;
        [ObservableProperty]

        private int trimestrielCount;

        [ObservableProperty]

        private ObservableCollection<GroupAbonnement> listeAbonnements;

        #endregion
    }
}
