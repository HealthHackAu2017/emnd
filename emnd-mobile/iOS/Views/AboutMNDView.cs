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
    [MvxFromStoryboard("AboutMNDView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "About")]
    public partial class AboutMNDView : MvxViewController<AboutMNDViewModel>

    {
        public AboutMNDView (IntPtr handle) : base (handle)
        {
        }
    }
}