/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
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
    public class ForgotPasswordViewModel : BaseViewModel, IForgotPasswordViewModel
    {
        public String Title
        {
            get { return "Forgot Password"; }
        }
        public String Description
        {
            get { return "Application Name"; }
        }

        private String _email;
        public String Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand SendCommand { get; set; }
        public ICommand SignInCommand { get; set; }

        [Preserve]
        public ForgotPasswordViewModel(IPopupsService _iPopupsService, 
            IUserServices _iUserServices,
            INavigationService _iNavigationService)
        {
            this._iUserServices = _iUserServices;
            this._iPopupsService = _iPopupsService;
            this._iNavigationService = _iNavigationService;

            SendCommand = new Command(Send);
            SignInCommand = new Command(SignIn);
        }

        private async void Send()
        {
            ResourceManager rm = new ResourceManager("LoginAppSample.AppResources.Localization.Resources", typeof(App).GetTypeInfo().Assembly);
            string AlertMessage = String.Empty;
            if (String.IsNullOrEmpty(Email))
            {
                AlertMessage = rm.GetString("EMAIL_REQUIRED");
            }
            else if (!IsValid(Email))
            {  
                AlertMessage = rm.GetString("INVALID_EMAIL_REQUIRED");
            }
            if (!String.IsNullOrEmpty(AlertMessage))
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), AlertMessage, rm.GetString("OK"));
                return;
            }
            var result = await _iUserServices.SignIn(User);
            if (result)
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), rm.GetString("FORGOT_PASSWORD_MESSAGE"), rm.GetString("OK"));
                await _iNavigationService.GoBack();
            }
            else
            {
                await _iPopupsService.DisplayAlert(rm.GetString("APPLICATION_NAME"), rm.GetString("ERROR_MESSAGE"), rm.GetString("OK"));
            }            
        }

        private async void SignIn()
        {
            await _iNavigationService.GoBack();
        }        
    }
}
