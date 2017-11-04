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
    [MvxFromStoryboard("ProfileView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "Account", TabName = "Account")]
    public partial class ProfileView : MvxTableViewController<ProfileViewModel>
    {
        public ProfileView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Profile View appear");
            //ShowSurveyListPage();
        }

        Xamarin.Forms.Page SurveyListPage;
        private void ShowSurveyListPage()
        {
            SurveyListPage = new SurveyListPage();
            var pageViewController = SurveyListPage.CreateViewController();
            this.NavigationController.PushViewController(pageViewController, false);
        }

        Xamarin.Forms.Page AboutPage;
        private void ShowAboutPage()
        {
            AboutPage = new AboutPageADA();
            var aboutPageViewController = AboutPage.CreateViewController();
            this.NavigationController.PushViewController(aboutPageViewController, true);
        }

    }
}
