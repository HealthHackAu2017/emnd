using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Emnd
{
    public class AboutViewModel : MvxViewModel
    {
        readonly IMvxNavigationService navigationService;

        public AboutViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            //Title = "About";

            OpenWebCommand = new Command(() => Plugin.Share.CrossShare.Current.OpenBrowser("https://xamarin.com/platform"));
        }

        //public AboutViewModel()
        //{
        //}

        public ICommand OpenWebCommand { get; }
    }
}
