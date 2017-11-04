using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Plugin.Toasts;

namespace Emnd
{
    public class SurveyViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public SurveyViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
