/// Mohamed Ali NOUIRA
/// http://www.sweetmit.com
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using LoginAppSample.Services;
using LoginAppSample.IServices;
using LoginAppSample.IViewModels;
using Xamarin.Forms.Popups;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Navigation;

namespace LoginAppSample.ViewModels
{
    public class ViewModelLocator
    {
        [Preserve]
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ISignInViewModel, SignInViewModel>();
            SimpleIoc.Default.Register<ISignUpViewModel, SignUpViewModel>();
            SimpleIoc.Default.Register<IForgotPasswordViewModel, ForgotPasswordViewModel>();

            SimpleIoc.Default.Register<IHomeViewModel, HomeViewModel>();
            SimpleIoc.Default.Register<IDetailsViewModel, DetailsViewModel>();

            SimpleIoc.Default.Register<IUserServices, UserServices>();
            SimpleIoc.Default.Register<IPopupsService, PopupsService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
        }

        public ISignInViewModel SignIn
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISignInViewModel>();
            }
        }

        public ISignUpViewModel SignUp
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISignUpViewModel>();
            }
        }

        public IForgotPasswordViewModel ForgotPassword
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IForgotPasswordViewModel>();
            }
        }

        public IHomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IHomeViewModel>();
            }
        }

        public IDetailsViewModel Details
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDetailsViewModel>();
            }
        }
    }
}
