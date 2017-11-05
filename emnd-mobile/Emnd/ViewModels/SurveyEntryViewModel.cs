using System.Collections.Generic;
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
            SectionName = "Start";
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
            SendAsCSVCommand = new MvxAsyncCommand(async () =>
            {
                App.Navigation.ToastMessage("Sending CSV", CurrentSurvey.ParticipantName);
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

            //this.SectionName = "**** " + parameter;
            RaisePropertyChanged(nameof(this.SectionName));
            RaisePropertyChanged(nameof(this.CurrentSection));
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
                var q = this.Survey.NewQuestions();
                this.D21 = q.Find(e => e.QuestionVariable.Equals("D21"));
                RaiseAllPropertiesChanged();
            }

            var sect = this.Survey.NewSections(); // this.Survey;
            this.CurrentSection = sect.Find(e => e.SectionName.Equals(this.SectionName));

            var sq = new List<SurveyQuestion>();
            //sq = this.Survey._questions;//.FindAll((obj) => obj.Section.Equals(this.SectionName));
            sq = this.Survey._questions.FindAll((obj) => (obj.Section == this.SectionName));
            this.SectionQuestions = sq;
                

            //// Questions in this section are
            foreach (SurveyQuestion q in sq)
            {
                Log.Information($"Section question q= {q.Question} s={q.Section}");
            }
        }
    }
}
