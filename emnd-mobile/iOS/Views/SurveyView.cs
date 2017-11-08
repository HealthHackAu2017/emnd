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
    [MvxFromStoryboard("SurveyView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Survey")]
    public partial class SurveyView : MvxViewController<SurveyViewModel>    
    {
        public SurveyView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Survey View appeared");
        }

    }
}