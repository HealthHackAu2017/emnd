using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Plugin.Toasts;

namespace Emnd
{
    public class ProfileViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public ProfileViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
