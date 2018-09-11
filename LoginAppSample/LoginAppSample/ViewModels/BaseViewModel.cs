/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
using LoginAppSample.Models;
using System.Net.Mail;
using LoginAppSample.IServices;
using Xamarin.Forms.Popups;
using System.ComponentModel;
using Xamarin.Forms.Navigation;
using System.Runtime.CompilerServices;

namespace LoginAppSample.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IUserServices _iUserServices;
        protected IPopupsService _iPopupsService;
        protected INavigationService _iNavigationService;

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
