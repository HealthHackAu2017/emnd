using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Serilog;

namespace Emnd.iOS
{
    [MvxFromStoryboard("MenuView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Menu")]
    public partial class MenuView : MvxViewController<MenuViewModel>
    {
        public MenuView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("MenuView appeared");
        }

    }
}