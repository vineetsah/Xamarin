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
using System.Windows.Input;
using Xamarin.Forms.Popups;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Navigation;

namespace LoginAppSample.ViewModels
{
    public class SignUpViewModel : BaseViewModel, ISignUpViewModel
    {
        private String _confirmPassword;
        public String ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public String Title
        {
            get { return "Sign Up"; }
        }
        public String Description
        {
            get { return "Application Name"; }
        }

        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        [Preserve]
        public SignUpViewModel(IPopupsService _iPopupsService, 
            IUserServices _iUserServices,
            INavigationService _iNavigationService)
        {
            this._iUserServices = _iUserServices;
            this._iPopupsService = _iPopupsService;            
            this._iNavigationService = _iNavigationService;

            SignUpCommand = new Command(SignUp);
            SignInCommand = new Command(SignIn);

            this.User = new User();
        }

        private async void SignIn()
        {
            await _iNavigationService.GoBack();
        }

        private async void SignUp()
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
            if (String.IsNullOrEmpty(User.Username))
            {
                AlertMessage += rm.GetString("USERNAME_REQUIRED");
            }
            if (String.IsNullOrEmpty(User.Password))
            {
                AlertMessage += rm.GetString("PASSWORD_REQUIRED");
            }
            else if(String.IsNullOrEmpty(ConfirmPassword) || !User.Password.ToLower().Equals(ConfirmPassword.ToLower()))
            {
                AlertMessage += rm.GetString("PASSWORD_CONFIRM_PASSWORD_REQUIRED");
            }
            if (!String.IsNullOrEmpty(AlertMessage))
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), AlertMessage, rm.GetString("OK"));
                return;
            }
            var result = await _iUserServices.SignUp(User);
            if (result)
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), String.Format(rm.GetString("CONFIRM_SIGN_UP_MESSAGE"), rm.GetString("APPLICATION_NAME")), rm.GetString("OK"));
                await _iNavigationService.NavigateTo("SignIn");
            }
            else
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), rm.GetString("ERROR_MESSAGE"), rm.GetString("OK"));
            }
        }
    }
}
