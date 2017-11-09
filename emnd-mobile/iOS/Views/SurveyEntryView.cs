using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Serilog;
using MvvmCross.Binding.iOS.Views;
using System.Collections.Generic;
using CoreGraphics;

namespace Emnd.iOS
{
    [MvxFromStoryboard("SurveyEntryView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Start")]
    public partial class SurveyEntryView : MvxViewController<SurveyEntryViewModel>
    {
        public SurveyEntryView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Log.Information("Survey Start load");

            this.NavigationItem.Title = "Start Here";

            var NavButton = new UIBarButtonItem();
            NavButton.Title = "START";
            NavButton.Clicked += delegate {
                ViewModel.ShowBodyMapCommand.Execute(null);
            };
            NavigationItem.RightBarButtonItem = NavButton;

            var HomeButton = new UIBarButtonItem();
            HomeButton.Title = "HOME";
            HomeButton.Clicked += delegate {
                this.TabBarController.SelectedIndex = 0;
            };
            NavigationItem.LeftBarButtonItem = HomeButton;

            var set = this.CreateBindingSet<SurveyEntryView, SurveyEntryViewModel>();
            set.Bind(ParticipantNameEntry).To(vm => vm.Survey.ParticipantName);
            set.Bind(ParticipantIdEntry).To(vm => vm.Survey.ParticipantID);
            set.Bind(WeightEntry).To(vm => vm.Survey.Weight);
            //set.Bind(SendButton).To(vm => vm.ShowBodyMapCommand);
            set.Apply();

            ViewModel.SectionName = "Wellbeing";
            ViewModel.Init();
            this.QuestionTable.Source = new SectionQuestionTableSource(this.QuestionTable, ViewModel.SectionQuestions, ViewModel);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Survey Start appeared");
        }

        protected class SectionQuestionTableSource : MvxTableViewSource
        {
            List<SurveyQuestion> TableSourceViewModel;
            SurveyEntryViewModel baseViewModel;
            public override nint RowsInSection(UITableView tableview, nint section) => ((TableSourceViewModel != null) ? TableSourceViewModel.Count : 0);
            public override nint NumberOfSections(UITableView tableView) => 1;

            public SectionQuestionTableSource(UITableView tableView, List<SurveyQuestion> viewModel, SurveyEntryViewModel baseViewModel) : base(tableView)
            {
                tableView.RegisterNibForCellReuse(UINib.FromName(SurveyQuestionViewCell.Key, NSBundle.MainBundle), SurveyQuestionViewCell.Key);
                this.TableSourceViewModel = viewModel;
                this.baseViewModel = baseViewModel;
            }

            protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
            {
                UITableViewCell cell = null;
                SurveyQuestion model = this.TableSourceViewModel[indexPath.Row];
                if (model != null)
                {
                    cell = (UITableViewCell)tableView.DequeueReusableCell(SurveyQuestionViewCell.Key);
                    if ((cell is SurveyQuestionViewCell) && !((SurveyQuestionViewCell)cell).IsBound)
                    {
                        ((SurveyQuestionViewCell)cell).BindModel(this.baseViewModel, model);
                    }
                }
                return cell;
            }
        }

    }
}