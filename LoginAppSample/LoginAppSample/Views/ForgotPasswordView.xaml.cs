﻿/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginAppSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgotPasswordView : ContentPage
	{
		public ForgotPasswordView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}