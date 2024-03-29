﻿using CommunityToolkit.Mvvm.ComponentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ritegeapp.ViewModels;
using System;
using System.Threading.Tasks;

namespace ritegeapp.Views
{
    public partial class CashRegisterListView : PopupPage
    {
        protected async override void OnAppearing()
        {
            base.OnAppearing();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(() => ((CashRegisterListViewViewModel)BindingContext).LoadList());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            //            Task.Run(async () =>
            //);

        }
        public CashRegisterListView(ObservableObject viewmodel)
        {
            InitializeComponent();
            BindingContext = new CashRegisterListViewViewModel(viewmodel);
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
