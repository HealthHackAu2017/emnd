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
            var NavButton = new UIBarButtonItem();
            NavButton.Title = "SAVE";
            NavButton.Clicked += async (object sender, EventArgs e) =>
            {
                await SaveSurveyAsync();
            };
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
            //if (ViewModel.
        }


        public async Task<bool> SaveSurveyAsync()
        {
            bool success = false;
            string ErrorMessage = "An unknown error occurred";
            //UserDialogs.Instance.ShowLoading("Authenticating");
            try
            {
                var SubmissionRequest = new SubmissionDTO
                {
                    UserId = 1
                };
                // Map the user answers
                foreach (var q in ViewModel.Survey._questions)
                {
                    var property = SubmissionRequest.GetType().GetProperty(q.QuestionVariable);
                    if (property != null)
                    {
                        property.SetValue(SubmissionRequest, q.AnswerValue, null);
                    }
                    else
                    {
                        Log.Information("Skipping " + q.QuestionVariable);
                    }
                }
                var json = SubmissionRequest.AsJsonString();
                Log.Information("Saving JSON to server " + json.AsJsonString());
                //return success;

                var network = new NetworkService();

                var submissionResult = await network.PostData<SubmissionResultDTO>("/submissions/submit/", json, "Saving...");
                if (submissionResult != null && submissionResult.Count > 0)
                {
                    //set session token
                    //SubmissionResultDTO result = submissionResult[0];
                    //Log.Information("Received result " + result);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Error("AsyncTryLoginCommand " + ex.StackTrace);
            }
            finally
            {
                //UserDialogs.Instance.HideLoading();
                Log.Information("Success");
                if (success)
                {
                    ShowResultPage();
                }
                if (!success)
                {
                    App.Navigation.ShowError(ErrorMessage);
                }
            }
            return success;
        }

        public void ShowResultPage()
        {
            this.TabBarController.SelectedIndex = 2;
        }


    }
}