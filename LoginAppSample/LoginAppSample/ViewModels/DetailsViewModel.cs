﻿/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using LoginAppSample.Models;
using Xamarin.Forms;
using LoginAppSample.IViewModels;
using System.Windows.Input;
using Xamarin.Forms.Popups;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Navigation;

namespace LoginAppSample.ViewModels
{
    public class DetailsViewModel : BaseViewModel, IDetailsViewModel
    {
        private SampleModel _mySampleModel;
        public SampleModel MySampleModel
        {
            get { return _mySampleModel; }
            set
            {
                _mySampleModel = value;
                OnPropertyChanged(nameof(MySampleModel));
            }
        }

        public ICommand SelectedItemCommand { get; set; }

        [Preserve]
        public DetailsViewModel(IPopupsService _iPopupsService,
            INavigationService _iNavigationService)
        {
            this._iPopupsService = _iPopupsService;
            this._iNavigationService = _iNavigationService;
            SelectedItemCommand = new Command<string>(SelectedItem);

            MySampleModel = _iNavigationService.GetParameters().GetValue<SampleModel>(nameof(SampleModel));
        }

        private async void SelectedItem(string param)
        {
            await _iPopupsService.DisplayAlert("SelectedItem", param, "Ok");
        }
    }
}
