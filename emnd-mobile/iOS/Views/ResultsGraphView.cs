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
    [MvxFromStoryboard("ResultsGraph")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Results")]
    public partial class ResultsGraphView : MvxViewController<ResultsGraphViewModel>

    {
        public ResultsGraphView(IntPtr handle) : base(handle)
        {
        }
    }
}