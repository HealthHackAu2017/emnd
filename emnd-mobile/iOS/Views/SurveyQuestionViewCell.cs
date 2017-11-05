using System;
using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Serilog;
using Xamarin.Forms;


namespace Emnd.iOS
{
    public partial class SurveyQuestionViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SurveyQuestionViewCell");
        public static readonly UINib Nib;
        public static UINavigationController NavigationController { get; set; }

        static SurveyQuestionViewCell()
        {
            Nib = UINib.FromName("SurveyQuestionViewCell", NSBundle.MainBundle);
        }

        protected SurveyQuestionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            //AnswerSlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);
        }

        // Bind manually when autobinding failed
        public bool IsBound;
        public void BindModel(SurveySectionViewModel vm, SurveyQuestion model)
        {
            Log.Information("Cell manual bind");
            this.ContentView.ClearsContextBeforeDrawing = true;
            AnswerSlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);

            QuestionLabel.Text = model.Question;
            MinLabel.Text = model.MinText;
            MaxLabel.Text = model.MaxText;

            AnswerSlider.MinValue = (float)model.MinValue;
            AnswerSlider.MaxValue = (float)model.MaxValue;
            AnswerSlider.Value = (float)model.DefaultValue;
            //UITapGestureRecognizer viewTapGR = new UITapGestureRecognizer(() =>
            //{
            //    //App.Navigation.ShowVideoPlaybackPage(vm);
            //    ShowVideoPlayback(vm);
            //});
            //this.AddGestureRecognizer(viewTapGR);


            this.SetNeedsDisplay();
        }
    }
}
