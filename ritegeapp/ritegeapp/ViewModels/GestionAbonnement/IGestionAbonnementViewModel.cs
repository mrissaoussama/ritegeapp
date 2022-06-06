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
    public interface IGestionAbonnementViewModel
    {
        public ObservableRangeCollection<GroupAbonnement> ListAbonnementToShow { get; set; }
        public ObservableRangeCollection<DateAbonnement> ListDateAbonnementToShow { get; set; }

        public  Task OnDataReceivedAsync(List<InfoAbonnementDTO> data);

        public void UpdateLists(List<GroupAbonnement> groupAbonnements, List<DateAbonnement> dateAbonnements);

        public Task<List<GroupAbonnement>> GroupByAbonnement();

        public void CalculateListTotal(List<GroupAbonnement> dto);
            public void ShowLoading();
            public void ShowNoFilterMessage();
        public void ShowDataView();
        public void ShowNoInternetMessage();
        public void ShowNoDataReceivedMessage();

        public void SearchText(object obj);

        public Task GetData();
        public void ChangeGroupByView(object obj);
        public void ClearFilter(object obj);
        public void OpenStatisticsPopup(object obj);
    }
}