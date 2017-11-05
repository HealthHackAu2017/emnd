using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Emnd
{
    public class ResultsGraphViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public ResultsGraphViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}

