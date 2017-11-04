using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace Emnd.iOS
{
    [MvxFromStoryboard("SurveyView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Survey")]
    public partial class SurveyView : MvxViewController<SurveyViewModel>    
    {
        public SurveyView (IntPtr handle) : base (handle)
        {
        }
    }
}