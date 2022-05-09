using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Services;
    using RitegeDomain.DTO;
using ritegeapp.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ritegeapp.ViewModels
{
    public class GestionRecetteStatisticsPopupViewModel : ExtendedBindableObject
    {
        public GestionRecetteStatisticsPopupViewModel(ObservableObject viewmodel)
        {
            parentvm = viewmodel;
            ListeRecettes = new ObservableCollection<InfoTicketDTO>(((GestionRecettesViewModel)parentvm).ListRecetteToShow);
            // SelectedRecette = ListeRecettes.First().ToString();
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
            foreach (var info in ListeRecettes)
            {
                TicketSum += info.MontantPaye;
                TicketTotal++;
            }
            TicketStationnementTotal = ListeRecettes.Count(x => x.TypeTicket == TypeTicket.TicketStationnement);
            TicketStationnementSum = ListeRecettes.Where(x => x.TypeTicket == TypeTicket.TicketStationnement).Sum(x => x.MontantPaye);
        }

        #region variables

        private int _ticketTotal;
        public int TicketTotal
        {
            get { return _ticketTotal; }
            set { _ticketTotal = value; RaisePropertyChanged(() => TicketTotal); }
        }

        private TypeTicket _typeTicket;
        public TypeTicket TypeTicket
        {
            get { return _typeTicket; }
            set { _typeTicket = value; RaisePropertyChanged(() => TypeTicket); }
        }
        private decimal _TicketStationnementSum;
        public decimal TicketStationnementSum
        {
            get { return _TicketStationnementSum; }
            set { _TicketStationnementSum = value; RaisePropertyChanged(() => TicketStationnementSum); }
        }
        private int _TicketStationnementTotal;
        public int TicketStationnementTotal
        {
            get { return _TicketStationnementTotal; }
            set { _TicketStationnementTotal = value; RaisePropertyChanged(() => TicketStationnementTotal); }
        }
        private decimal _TicketSum;
        public decimal TicketSum
        {
            get { return _TicketSum; }
            set { _TicketSum = value; RaisePropertyChanged(() => TicketSum); }
        }
        private ObservableCollection<InfoTicketDTO> _listeRecettes;
        public ObservableCollection<InfoTicketDTO> ListeRecettes
        {
            get { return _listeRecettes; }
            set { _listeRecettes = value; RaisePropertyChanged(() => ListeRecettes); }
        }

        #endregion
    }
}
