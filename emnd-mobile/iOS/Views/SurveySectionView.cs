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
    [MvxFromStoryboard("SurveySectionView")]
    [MvxChildPresentation]
    public partial class SurveySectionView : MvxViewController<SurveySectionViewModel>
    {
        public SurveySectionView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            // TODO: base view to get rid of keyboard

            base.ViewDidLoad();
            Log.Information("Survey Section load");

            var set = this.CreateBindingSet<SurveySectionView, SurveySectionViewModel>();
            set.Bind(SectionNameLabel).To(vm => vm.SectionName);
            set.Bind(SectionInfoLabel).To(vm => vm.CurrentSection.SectionInfo);
            set.Apply();

            //this.FeedbackTextField.ShouldReturn += SearchBar_ShouldReturn;
            //this.FeedbackButton.TouchUpInside += FeedbackButton_TouchUpInside;
            //RewardTable.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
        }

    }
}