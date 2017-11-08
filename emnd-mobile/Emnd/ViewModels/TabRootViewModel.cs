using System.Threading.Tasks;
using System;
using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using MvvmCross.Platform.Platform;

namespace Emnd
{
    public class TabRootViewModel : MvxViewModel
    {
        public static TabRootViewModel Instance { get; set; }
        private readonly IMvxNavigationService _navigationService;

        public TabRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            App.Instance._nav = _navigationService;
            Instance = this;
            ShowRootViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
        }

        public IMvxAsyncCommand ShowRootViewModelsCommand { get; private set; }

        private async Task ShowInitialViewModels()
        {
            var tasks = new List<Task>();
            tasks.Add(_navigationService.Navigate<MenuViewModel>());
            //tasks.Add(_navigationService.Navigate<BodyViewModel>());
            //tasks.Add(_navigationService.Navigate<SurveyViewModel>());
            //tasks.Add(_navigationService.Navigate<ProfileViewModel>());
            tasks.Add(_navigationService.Navigate<SurveyEntryViewModel>());
            tasks.Add(_navigationService.Navigate<ResultsGraphViewModel>());
            tasks.Add(_navigationService.Navigate<AboutMNDViewModel>());
            await Task.WhenAll(tasks);
        }

        private int _itemIndex;

        public int ItemIndex
        {
            get { return _itemIndex; }
            set
            {
                if (_itemIndex == value) return;
                _itemIndex = value;
                MvxTrace.Trace("Tab item changed to {0}", _itemIndex.ToString());
                RaisePropertyChanged(() => ItemIndex);
            }
        }
    }
}
