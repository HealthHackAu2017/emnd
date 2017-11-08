using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Serilog;
using System.Threading.Tasks;

namespace Emnd.iOS
{
    [MvxFromStoryboard("BodyView")]
    [MvxChildPresentation]
    public partial class BodyView : MvxViewController<BodyViewModel>
    {
        private int FirstLoad = 0;
        public BodyView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Log.Information("MenuView load");

            NavigationItem.Title = ViewModel.SectionName;
            var NavButton = new UIBarButtonItem();
            NavButton.Title = "SAVE";
            NavButton.Clicked += SaveButton_Clicked;
            NavigationItem.RightBarButtonItem = NavButton;


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

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Body View appear");

            if (FirstLoad++ > 1)
            {
                if (NeckComplete.Hidden)
                {
                    NeckComplete.Hidden = false;
                }
                else if (LungComplete.Hidden)
                {
                    LungComplete.Hidden = false;
                }
            }
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var saved = await ViewModel.SaveSurveyAsync();
            if (saved)
            {
                ShowResultPage();
            }
        }

        public void ShowResultPage()
        {
            this.TabBarController.SelectedIndex = 2;
        }


    }
}