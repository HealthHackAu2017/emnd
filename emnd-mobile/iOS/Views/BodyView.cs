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
    [MvxFromStoryboard("BodyView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "BodyMap")]
    public partial class BodyView : MvxViewController<BodyViewModel>
    {
        public BodyView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Body View appeared");
        }

    }
}