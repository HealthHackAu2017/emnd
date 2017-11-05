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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Log.Information("MenuView load");

            //var NavButton = new UIBarButtonItem();
            //NavButton.Title = "Add +";
            //NavButton.Clicked += delegate {
            //    ShowStartTab();
            //};
            //NavigationItem.RightBarButtonItem = NavButton;

            //HeadButton.TouchUpInside += (sender, e) =>
            //{
            //    App.Instance._nav.Navigate<SurveySectionViewModel, string>("Mental Health");
            //};
            //SalivaButton.TouchUpInside += (sender, e) =>
            //{
            //    App.Instance._nav.Navigate<SurveySectionViewModel, string>("Saliva");
            //};
            Lung1Button.TouchUpInside += (sender, e) =>
            {
                //App.Navigation.ToastMessage("Click", "Lung");
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Breathing");
            };
            Throat1Button.TouchUpInside += (sender, e) =>
            {
                //App.Navigation.ToastMessage("Click", "Throat");
                App.Instance._nav.Navigate<SurveySectionViewModel, string>("Mental Health");
            };
        }

    }
}