using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Plugin.Messaging;
using Serilog;

// Observables for current section and overall progress
namespace Emnd
{
    public class SurveyEntryViewModel : MvxViewModel<string>
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseViewModelCommand { get; private set; }
        public IMvxAsyncCommand SendAsCSVCommand { get; private set; }
        public IMvxAsyncCommand ShowBodyMapCommand { get; private set; }
        public static Survey CurrentSurvey { get; set; }
        public Survey Survey { get; set; }

        public SurveyEntryViewModel()
        {
            Log.Information("SurveyEntryViewModel constructor");

            _navigationService = App.Instance._nav;
            SectionName = "Wellbeing";
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
            SendAsCSVCommand = new MvxAsyncCommand(async () =>
            {
                App.Navigation.ToastMessage("Sending CSV", CurrentSurvey.ParticipantName);
            });
            ShowBodyMapCommand = new MvxAsyncCommand(async () =>
            {
                App.Instance._nav.Navigate<BodyViewModel>();
            });

            Init();
        }

        public SurveyEntryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
        }

        public string SectionName { get; set; }
        public SurveySection CurrentSection { get; set; }
        public List<SurveyQuestion> SectionQuestions { get; set; }
        public override void Prepare(string parameter)
        {
            Log.Information("Prepare view model with " + parameter);
            this.SectionName = parameter;
            Init();
            RaisePropertyChanged(nameof(this.SectionName));
            RaisePropertyChanged(nameof(this.CurrentSection));
        }

        public void Init()
        {
            if (CurrentSurvey == null)
            {
                CurrentSurvey = new Survey();
            }

            if (this.Survey == null)
            {
                this.Survey = CurrentSurvey;
            }

            this.CurrentSection = CurrentSurvey._sections.Find(e => e.SectionName.Equals(this.SectionName));

            var sq = new List<SurveyQuestion>();
            sq = CurrentSurvey._questions.FindAll((obj) => (obj.Section == this.SectionName));
            this.SectionQuestions = sq;
            foreach (SurveyQuestion q in sq)
            {
                Log.Information($"Section question q= {q.Question} s={q.Section}");
            }
        }

        public async Task<bool> SaveSurveyAsync()
        {
            string ErrorMessage = "An unknown error occurred";
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentSurvey.ParticipantID))
                {
                    App.Navigation.ShowError("Please fill in your Participant ID on the start page");
                    return false;
                }
                AppSettings.ParticipantName = CurrentSurvey.ParticipantName;
                AppSettings.ParticipantId = CurrentSurvey.ParticipantID;
                AppSettings.ParticipantWeight = CurrentSurvey.Weight;

                bool isServerAvailable = false;
                if (isServerAvailable)
                {
                    return await UploadToServerAsync();
                }
                else
                {
                    SendSurveyAsEmail();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Log.Error("AsyncTryLoginCommand " + ex.StackTrace);
                return false;
            }
            finally
            {
            }
            return true;
        }


        void SendSurveyAsEmail()
        {
            Log.Information("==============================================");
            Log.Information("=== SURVEY RESPONSE ===");

            var errorMessage = string.Empty;
            try
            {
                var surveyCSV = CurrentSurvey.AsEmail();
                Log.Information($"Attach log file request {AppSettings.LastLogFile}");
                Log.Information($"Attach survey response {surveyCSV}");
                //Log.CloseAndFlush();

                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmailAttachments)
                {
                    // Construct email with attachment on Android.
                    Log.Information("Building message");

                    var email = new EmailMessageBuilder()
                        .To("a.henders@uq.edu.au")
                        .Subject($"SHOWmndME Survey Response for {AppSettings.ParticipantName}").Body($"{surveyCSV}\n\n\n")
                          //.WithAttachment(AppSettings.LastLogFile, "text/*")
                          .Build();

                    InvokeOnMainThread(() =>
                    {
                        Log.Information("Sending email");
                        emailMessenger.SendEmail(email);
                    });
                }
                else
                {
                    errorMessage = "Your device can't send email attachments.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }


            if (!string.IsNullOrEmpty(errorMessage))
            {
                App.Navigation.ShowError(errorMessage);
            }
            //App.Navigation.ShowAlert(surveyCSV);

        }

        public async Task<bool> UploadToServerAsync()
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
                foreach (var q in CurrentSurvey._questions)
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
                    //ShowResultPage();
                }
                if (!success)
                {
                    App.Navigation.ShowError(ErrorMessage);
                }
            }
            return success;
        }

    }
}
