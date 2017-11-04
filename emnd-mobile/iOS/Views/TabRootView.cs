using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Serilog;

namespace Emnd.iOS
{
    [MvxFromStoryboard("TabRootView")]
    [MvxRootPresentation(WrapInNavigationController = false)]
    public partial class TabRootView : MvxTabBarViewController<TabRootViewModel>
    {
        public TabRootView (IntPtr handle) : base (handle)
        {
        }

        private bool _isPresentedFirstTime = true;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewModel != null && _isPresentedFirstTime)
            {
                _isPresentedFirstTime = false;
 
                Log.Information("Setting up tabs");
                ViewModel.ShowRootViewModelsCommand.ExecuteAsync(null);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (TabRootTabBar != null)
            {
                //TabRootTabBar.UnselectedItemTintColor = UIColor.White; iOS 10+ only
                //TabRootTabBar.ItemSelected += (sender, e) =>
                //{
                //    Log.Information("Selected tab = " + TabRootTabBar.SelectedItem.Title);
                //};
            }
        }
    }
}