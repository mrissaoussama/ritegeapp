using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Rg.Plugins.Popup.Services;
    using RitegeDomain.DTO;
using RitegeDomain.Model;
using ritegeapp.Services;
using ritegeapp.Utils;
using ritegeapp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using MvvmHelpers;

namespace ritegeapp.ViewModels
{
    [INotifyPropertyChanged]
    public partial class GestionAbonnementViewModel : IGestionAbonnementViewModel
    {
        #region variables   
        public List<InfoAbonnementDTO> ListDto = new();
        [ObservableProperty]
        public ObservableRangeCollection<GroupAbonnement> listAbonnementToShow = new();
        public ObservableRangeCollection<GroupAbonnement> listAbonnement = new();

        [ObservableProperty]
        public ObservableRangeCollection<DateAbonnement> listDateAbonnementToShow = new();
        public ObservableRangeCollection<DateAbonnement> listDateAbonnement = new();

        [ObservableProperty]
        private string searchTextBox = "";
        [ObservableProperty]
        private DateTime dateStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        [ObservableProperty]
        private DateTime dateEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(6);
        [ObservableProperty]
        private bool abonnementSortMode;
        [ObservableProperty]
        private bool dateAbonnementSortMode;
        [ObservableProperty]
        private decimal totalMoney;
        
        [ObservableProperty]
        private ViewStateManager stateManager=new();
        #endregion
        IDataService dataService;
        private int searchLength;
        public GestionAbonnementViewModel()
        {
            dataService = DependencyService.Get<IDataService>();
            AbonnementSortMode = true;
            DateAbonnementSortMode = false;
        }
        public async Task OnDataReceivedAsync(List<InfoAbonnementDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                await Device.InvokeOnMainThreadAsync(() => StateManager.ShowNoDataReceivedMessage());
            }
            else
            {
                ListDto = (data);
                ListAbonnementToShow.Clear();
                ListDateAbonnementToShow.Clear();
                listAbonnement.Clear();
                listDateAbonnement.Clear();
                UpdateLists(await GroupByAbonnement(), await GroupByDateAbonnement());
            }
        }

        public void UpdateLists(List<GroupAbonnement> groupAbonnements, List<DateAbonnement> dateAbonnements)
        {
            listAbonnement.AddRange(groupAbonnements);
            listDateAbonnement.AddRange(dateAbonnements);

            ListAbonnementToShow = new(listAbonnement);
            ListDateAbonnementToShow = new(listDateAbonnement);
            CalculateListTotal(ListAbonnementToShow.ToList());
            StateManager.ShowDataView();
        }

        public async Task<List<GroupAbonnement>> GroupByAbonnement()
        {
            var result = ListDto;
            var Dto = new List<GroupAbonnement>(listAbonnementToShow);
            result = result.GroupBy(p => p.NomPrenomAbonne).Select(grp => grp.FirstOrDefault()).ToList();
            for (int i = 0; i < result.Count; i++)
            { var abonnements = Dto.Where(Abonnement => Abonnement.NomPrenomAbonne == result[i].NomPrenomAbonne).ToList();
                if (abonnements.Count > 0)
                {
                    abonnements[0].ListAbonnement.Add(result[i]);
                }
                else
                {
                    GroupAbonnement c = new GroupAbonnement(ListDto, result[i].NomPrenomAbonne);
                    Dto.Add(c);
                }
            }
            return Dto;
        }

        public void CalculateListTotal(List<GroupAbonnement> dto)
        {
            TotalMoney = dto.Sum(x => x.AbonnementTotal);
        }

        public async Task<List<DateAbonnement>> GroupByDateAbonnement()
        {
            var result = ListDto; 
            var Dto = new List<DateAbonnement>();

            result = result.GroupBy(p => p.DateActivation).Select(grp => grp.FirstOrDefault()).ToList();
            foreach (var x in result)
            { var abonnement = ListDateAbonnementToShow.Where(Abonnement => Abonnement.dateStart == x.DateActivation).ToList();
                if (abonnement.Count > 0)
                {
                    abonnement[0].ListAbonnement.Add(x);
                }
                else
                {
                    DateAbonnement c = new DateAbonnement(ListDto, x.DateActivation);
                    Dto.Add(c);
                }
            }
            return Dto;
        }
    

        [ICommand]
        public async void SearchText(object obj)
        {
            await GetData();
        }
        [ICommand]
        public async Task GetData()
        {
            if ((Application.Current as App).IsOnline)
            {
                StateManager.ShowLoading();
                var list = await dataService.GetAbonnementData(DateStart, DateEnd.AddDays(1).AddTicks(-1), SearchTextBox);
                await OnDataReceivedAsync(list);
            }
            else
            if (ListDto.Count == 0)
                StateManager.ShowNoInternetView();
        }
        [ICommand]
        public void ChangeGroupByView(object obj)
        {
            AbonnementSortMode = !AbonnementSortMode;
            DateAbonnementSortMode = !DateAbonnementSortMode;
        }
        [ICommand]
        public async void ClearFilter(object obj)
        {
            DateStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateEnd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(6);
            SearchTextBox = "";
            await GetData();
        }
        [ICommand]
        public async void OpenStatisticsWindow(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new GestionAbonnementStatisticsPopup(this));
        }
        [ICommand]
        public async void TextChanged(object obj)
        {
            Debug.WriteLine("changed");
            StateManager.ShowLoading();
                ListAbonnementToShow.Clear();
                    ListDateAbonnementToShow.Clear();
            if (!string.IsNullOrEmpty(SearchTextBox))
            {
                for (var i = 0; i < listAbonnement.Count; i++)
                {
                    if (listAbonnement[i].NomPrenomAbonne.ToLower().Contains(SearchTextBox.ToLower()))
                    {
                        ListAbonnementToShow.Add(listAbonnement[i]);
                    }
                }
for (var i = 0; i < listAbonnement.Count; i++)
{
for (var abonnement = 0; abonnement < listDateAbonnement[i].ListAbonnement.Count; abonnement++)
{
if (listDateAbonnement[i].ListAbonnement[abonnement].NomPrenomAbonne.ToLower().Contains(SearchTextBox.ToLower()))
{
    var item = listDateAbonnement[i].ListAbonnement[abonnement];
    if (ListDateAbonnementToShow.ElementAtOrDefault(i) == null)
    {
        ListDateAbonnementToShow.Add(listDateAbonnement[i]);
        ListDateAbonnementToShow[ListDateAbonnementToShow.Count-1].ListAbonnement.Clear();
    }
    ListDateAbonnementToShow[ListDateAbonnementToShow.Count - 1].ListAbonnement.Add(item);
}
}

}
            }
            if (string.IsNullOrEmpty(SearchTextBox))
            {
                ListAbonnementToShow = new(listAbonnement);
                ListDateAbonnementToShow = new(listDateAbonnement);

            }
            ////else
            ////{
            ////    searchLength = searchTextBox.Length;
            ////    ListAbonnementToShow = listAbonnement;
            ////    ListDateAbonnementToShow = listDateAbonnement;
            ////}
            CalculateListTotal(ListAbonnementToShow.ToList());
                StateManager.ShowDataView();
            }

        }
    }
