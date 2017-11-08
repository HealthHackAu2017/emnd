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
 
        static SurveyQuestionViewCell()
        {
            Nib = UINib.FromName("SurveyQuestionViewCell", NSBundle.MainBundle);
        }

        protected SurveyQuestionViewCell(IntPtr handle) : base(handle)
        {
        }

        public bool IsBound = false;
        public void BindModel(SurveyEntryViewModel vm, SurveyQuestion model)
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

            AnswerSlider.ValueChanged += (sender, e) =>
            {
                Log.Information($"Setting value for {model.QuestionVariable} to {AnswerSlider.Value}");
                model.AnswerValue = AnswerSlider.Value;
            };
            this.SetNeedsDisplay();
        }
    }
}
