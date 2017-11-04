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
    // Switch to     [MvxChildPresentation]
    [MvxFromStoryboard("SurveyEntryView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Start")]
    public partial class SurveyEntryView : MvxViewController<SurveyEntryViewModel>
    {
        public SurveyEntryView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            // TODO: base view to get rid of keyboard

            base.ViewDidLoad();
            Log.Information("Survey Start load");

            this.NavigationItem.Title = "Start Here";
            MySlider.SetThumbImage(UIImage.FromFile("Diary.png"), UIControlState.Normal);

            ViewModel.Init();
            var set = this.CreateBindingSet<SurveyEntryView, SurveyEntryViewModel>();
            set.Bind(ParticipantNameEntry).To(vm => vm.Survey.ParticipantName);
            set.Bind(ParticipantIdEntry).To(vm => vm.Survey.ParticipantID);
            set.Bind(WeightEntry).To(vm => vm.Survey.Weight);
            set.Apply();

            //this.FeedbackTextField.ShouldReturn += SearchBar_ShouldReturn;
            //this.FeedbackButton.TouchUpInside += FeedbackButton_TouchUpInside;
            //RewardTable.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Survey Start appeared");
        }


        Xamarin.Forms.Page SurveyListPage;
        private void ShowSurveyListPage()
        {
            SurveyListPage = new SurveyListPage();
            var pageViewController = SurveyListPage.CreateViewController();
            this.NavigationController.PushViewController(pageViewController, false);
        }

    }
}