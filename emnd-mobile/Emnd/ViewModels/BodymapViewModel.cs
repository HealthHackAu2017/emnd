using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Emnd
{
    public class BodymapViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public string UserName { get; set; }

        public BodymapViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
