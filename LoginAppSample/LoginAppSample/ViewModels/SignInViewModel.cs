/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
using LoginAppSample.Models;
using Xamarin.Forms;
using LoginAppSample.IServices;
using System.Resources;
using System.Reflection;
using LoginAppSample.IViewModels;
using Xamarin.Forms.Popups;
using System.Windows.Input;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Navigation;

namespace LoginAppSample.ViewModels
{
    public class SignInViewModel : BaseViewModel, ISignInViewModel
    {        
        public String Title
        {
            get { return "Sign In"; }
        }        
        public String Description
        {
            get { return "Application Name"; }
        }        

        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }

        [Preserve]
        public SignInViewModel(IPopupsService _iPopupsService,
            IUserServices _iUserServices,
            INavigationService _iNavigationService)
        {
            this._iUserServices = _iUserServices;
            this._iPopupsService = _iPopupsService;
            this._iNavigationService = _iNavigationService;

            SignInCommand = new Command(SignIn);
            SignUpCommand = new Command(SignUp);
            ForgotPasswordCommand = new Command(ForgotPassword);

            this.User = new User();
        }

        private async void SignIn()
        {
            ResourceManager rm = new ResourceManager("LoginAppSample.AppResources.Localization.Resources", typeof(App).GetTypeInfo().Assembly);

            string AlertMessage = String.Empty;
            if (String.IsNullOrEmpty(User.Email))
            {
                AlertMessage = rm.GetString("EMAIL_REQUIRED");
            }
            else if (!IsValid(User.Email))
            {
                AlertMessage = rm.GetString("INVALID_EMAIL_REQUIRED");
            }
            if (String.IsNullOrEmpty(User.Password))
            {
                AlertMessage += rm.GetString("PASSWORD_REQUIRED");
            }
            if (!String.IsNullOrEmpty(AlertMessage))
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), AlertMessage, rm.GetString("OK"));
                return;
            }
            var result = await _iUserServices.SignIn(User);
            if (result)
            {
                NavigationParameters _navigationParameters = new NavigationParameters();
                _navigationParameters.Add(nameof(User), User);
                await _iNavigationService.NavigateTo("HomeView", _navigationParameters);
            }
            else
            {                
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), rm.GetString("ERROR_MESSAGE"), rm.GetString("OK"));
            }    
        }

        private async void SignUp()
        {
            await _iNavigationService.NavigateTo("SignUpView");
        }

        private async void ForgotPassword()
        {
            await _iNavigationService.NavigateTo("ForgotPasswordView");
        }        
    }
}
