using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Emnd
{
    public class AboutMNDViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public AboutMNDViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}

