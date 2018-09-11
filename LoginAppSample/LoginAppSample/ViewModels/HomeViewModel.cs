﻿/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using System;
using LoginAppSample.Models;
using Xamarin.Forms;
using LoginAppSample.IViewModels;
using System.Windows.Input;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Navigation;
using System.Collections.Generic;

namespace LoginAppSample.ViewModels
{
    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        private String _welcomeText;
        public String WelcomeText
        {
            get { return _welcomeText; }
            set
            {
                _welcomeText = value;
                OnPropertyChanged(nameof(WelcomeText));
            }
        }

        public ICommand NavigateToCommand { get; set; }

        [Preserve]
        public HomeViewModel(INavigationService _iNavigationService)
        {
            this._iNavigationService = _iNavigationService;
            NavigateToCommand = new Command<String>(NavigateTo);

            User = _iNavigationService.GetParameters().GetValue<User>(nameof(User));
            WelcomeText = "Welcome " + User.Email + " to LoginAppSample MVVMLight App !";
        }

        private async void NavigateTo(string param)
        {
            var navigationParams = new NavigationParameters();
            var MySampleModel = new SampleModel() { Id = 0, Name = "LoginAppSample MVVMLight App Parameter", Items = new List<string>() { "Items 1", "Item 2", "Item 3" } };

            navigationParams.Add(nameof(SampleModel), MySampleModel);
            await _iNavigationService.NavigateTo(param, navigationParams);
        }
    }
}
