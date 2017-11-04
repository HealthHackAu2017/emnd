using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Serilog;

// Observables for current section and overall progress
namespace Emnd
{
    public class SurveyEntryViewModel : MvxViewModel<string>
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseViewModelCommand { get; private set; }
        public IMvxAsyncCommand SendAsCSVCommand { get; private set; }
        public static Survey CurrentSurvey { get; set; }
        public Survey Survey { get; set; }
        public SurveyQuestion D21 { get; set; }

        public SurveyEntryViewModel()
        {
            Log.Information("SurveyEntryViewModel constructor");

            _navigationService = App.Instance._nav;
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
            SendAsCSVCommand = new MvxAsyncCommand(async () => {
                App.Navigation.ToastMessage("Sending CSV", CurrentSurvey.ParticipantName);                                            
            });

            Init();
        }

        public SurveyEntryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
        }

        public string SectionName = "Start";
        public override void Prepare(string parameter)
        {
            Log.Information("Prepare view model with " + parameter);
            SectionName = parameter;
            Init();
        }

        public void Init()
        {
            if (CurrentSurvey == null)
            {
                CurrentSurvey = new Survey();
                CurrentSurvey.ParticipantName = "John";
                CurrentSurvey.ParticipantID = "1234";
                CurrentSurvey.Weight = 74.0;

            }

            if (this.Survey == null)
            {
                this.Survey = CurrentSurvey;
                var q = Emnd.Survey.NewQuestions();
                this.D21 = q.Find(e => e.QuestionVariable.Equals("D21"));
                RaiseAllPropertiesChanged();
            }

        }
    }
}
