using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ritegeapp.Services
{
    public partial class ViewStateManager:ObservableObject
    {
        [ObservableProperty]
        private bool showNoFilterResultLabel, listIsRefreshing, resetFilterButton, showLoadingIndicator, showData, showNoDataReceived, showNoInternetLabel, showTotal, canTapFilterImages, canClickCaissierList, canClickParkingList, canClickParkingOrCashRegister, cashRegisterIsLoading, parkingIsLoading, isLoading,isRefreshing;
        public void ShowLoading()
        {
            canClickParkingList = false;
            CanClickCaissierList = false;
            ShowTotal = false;
            ShowLoadingIndicator = true;
            ShowNoFilterResultLabel = false;
            ShowData = false;
            ShowNoDataReceived = false;
            canClickParkingOrCashRegister = false;
            CashRegisterIsLoading = true;
            parkingIsLoading = true;
            isLoading = true;
            IsRefreshing = true;
        }
        public void ShowNoFilterMessage()
        {        canClickParkingList = true;

            CanClickCaissierList = true;
            canClickParkingOrCashRegister = true;
            CashRegisterIsLoading = false;
            parkingIsLoading = false;
            isLoading = false;
            IsRefreshing = false;
            ShowNoInternetLabel = false;
            ShowTotal = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = true;
            ShowData = false;
            ShowNoDataReceived = false;
        }
        public void ShowDataView()
        {
            canClickParkingOrCashRegister = true;
            CashRegisterIsLoading = false;
            parkingIsLoading = false;
            isLoading = false;
            IsRefreshing = false;
            CanClickCaissierList = true;
            canClickParkingList = true;
            CanTapFilterImages = true;
            ShowTotal = true;
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = true;
            ShowNoDataReceived = false; 
            canClickParkingOrCashRegister = true;
        }
        public void ShowNoInternetView()
        {
            canClickParkingOrCashRegister = true;
            CashRegisterIsLoading = false;
            parkingIsLoading = false;
            isLoading = false;
            IsRefreshing = false;
            CanClickCaissierList = true;
            canClickParkingList = true;

            ShowNoInternetLabel = true;
            ShowTotal = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = false;
            ShowNoDataReceived = false; 
            canClickParkingOrCashRegister = false;
        }
        public void ShowNoDataReceivedMessage()
        {CanClickCaissierList = true; canClickParkingList = true;

            ShowTotal = false;
            ShowNoInternetLabel = false;
            ShowLoadingIndicator = false;
            ShowNoFilterResultLabel = false;
            ShowData = false;
            ShowNoDataReceived = true;
            canClickParkingOrCashRegister = false;
            canClickParkingOrCashRegister = true;
            CashRegisterIsLoading = false;
            parkingIsLoading = false;
            isLoading = false;
            IsRefreshing = false;
        }
    }
}
