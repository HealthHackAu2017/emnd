using System;
using Foundation;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using Serilog;
using Xamarin.Forms;
using CoreGraphics;

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
            this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

            AnswerSlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);

            QuestionLabel.Text = model.Question;
            MinLabel.Text = model.MinText;
            MaxLabel.Text = model.MaxText;

            AnswerSlider.MinValue = (float)model.MinValue;
            AnswerSlider.MaxValue = (float)model.MaxValue;
            AnswerSlider.Value = model.Answered ? (float)model.AnswerValue : (float)model.DefaultValue;

            ProgressView.Progress = AnswerSlider.Value / AnswerSlider.MaxValue;

            // Render the change continuously and not just when released
            //AnswerSlider.Continuous = true;
            //ProgressView.ProgressTintColor = AnswerSlider.MinimumTrackTintColor;
            //ProgressView.Hidden = true;

            AnswerSlider.ValueChanged += (sender, e) =>
            {
                Log.Information($"Setting value for {model.QuestionVariable} to {AnswerSlider.Value}");
                model.Answered = true;
                model.AnswerValue = AnswerSlider.Value;
                ProgressView.Progress = (AnswerSlider.Value / AnswerSlider.MaxValue);
            };
            this.SetNeedsDisplay();
        }

        //bool IsLaidOut = false;
        public override void LayoutSubviews()
        {
            //Log.Information($"Cell layout subviews IsLaidOut={IsLaidOut}");
            base.LayoutSubviews();

            //UpdateFrame();
        }

        private void UpdateFrame()
        {
            CGRect frame = AnswerSlider.Frame;
            frame.Y = frame.Y + 15;
            ProgressView.Frame = frame;
            CGAffineTransform transform = CGAffineTransform.MakeScale(1.0f, 20.0f);
            transform.TransformSize(ProgressView.Frame.Size);
            ProgressView.Transform = transform;
        }
    }
}
