using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Plugin.Toasts;

namespace Emnd
{
    public class BodyViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public BodyViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
