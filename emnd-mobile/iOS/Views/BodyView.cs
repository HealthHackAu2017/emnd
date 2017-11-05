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
    [MvxChildPresentation]
    //[MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "BodyMap")]
    public partial class BodyView : MvxViewController<BodyViewModel>
    {
        public BodyView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Log.Information("MenuView load");

             NavigationItem.Title = ViewModel.SectionName;

            HeadButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Mental Health");
            };
            ThroatButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Saliva");
            };
            LungButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Breathing");
            };
            DigestionButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Digestion");
            };
            LeftArmButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Right arm");
            };
            RightArmButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Left arm");
            };
            LeftLegButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Right leg");
            };
            RightLegButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Left leg");
            };
            TorsoButton.TouchUpInside += (sender, e) =>
            {
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Torso");
            };
        }

    }
}