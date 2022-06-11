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
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ritegeapp.ViewModels
{
    public partial class LoginVM : ObservableObject
    {
        #region variables

        [ObservableProperty]
        private string email, password, errorText;

        #endregion

        IDataService dataService;
        [ObservableProperty]

        public bool canClickButtonOrFields, showError;
        [ObservableProperty]

        public bool isLoading;

        public LoginVM()
        {
            dataService = DependencyService.Get<IDataService>();
            Email = String.Empty;
            Password = String.Empty;
            HideLoading();
            // InitData();
        }

        public void ShowLoading()
        {
            CanClickButtonOrFields = false;

            IsLoading = true;
        }
        public void HideLoading()
        {
            CanClickButtonOrFields = true;

            IsLoading = false;
        }

        [ICommand]

        public async Task GetData()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                ShowError = true;
                ErrorText = "Verifier la connexion";
            }
            if (string.IsNullOrEmpty(Email))
            {
                ShowError = true;

                ErrorText = "Email obligatoire";

                return;
            }
            if (!new EmailAddressAttribute().IsValid(Email))
            {
                ShowError = true;

                ErrorText = "Email invalide";

                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ShowError = true;

                ErrorText = "Mot de passe obligatoire";
                return;

            }
            IsLoading = true;
            CanClickButtonOrFields = false;
            ShowError = false;
            var token = await dataService.Login(Email, Password);
         
            if (string.IsNullOrEmpty(token))
            {
                ShowError = true;
                IsLoading = false;
                CanClickButtonOrFields = true;
                ErrorText = "Email ou mot de passe incorrecte";
                return;
            }
            try
            { 
                MessagingCenter.Send(Xamarin.Forms.Application.Current, "SwitchToDashboardView");
            }
            catch(Exception e)
            {

            }
        
        }

    }
}