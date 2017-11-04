using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Plugin.Toasts;

namespace Emnd
{
    public class DashboardViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public string UserName { get; set; }

        public DashboardViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            UserName = "Hello binding";
        }
    }
}
