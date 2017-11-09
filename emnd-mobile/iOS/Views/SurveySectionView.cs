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
            base.ViewDidLoad();
            Log.Information("Survey Section load");

            NavigationItem.Title = ViewModel.SectionName;


            var set = this.CreateBindingSet<SurveySectionView, SurveySectionViewModel>();
            set.Bind(SectionInfoLabel).To(vm => vm.CurrentSection.SectionInfo);
            set.Apply();

            this.QuestionTable.Source = new SectionQuestionTableSource(this.QuestionTable, ViewModel.SectionQuestions, ViewModel);

            this.SkipButton.TouchUpInside += (sender, e) =>
            {
                ViewModel.CurrentSection.Answered = false;
                ViewModel.CurrentSection.SectionSkipped = true;
                ViewModel.CloseViewModelCommand.Execute(null);
            };
            this.DoneButton.TouchUpInside += (sender, e) =>
            {
                ViewModel.CurrentSection.Answered = true;
                ViewModel.CurrentSection.SectionSkipped = false;
                ViewModel.CloseViewModelCommand.Execute(null);
            };
            //this.FeedbackTextField.ShouldReturn += SearchBar_ShouldReturn;
            //this.FeedbackButton.TouchUpInside += FeedbackButton_TouchUpInside;
            //RewardTable.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
        }

        protected class SectionQuestionTableSource : MvxTableViewSource
        {
            List<SurveyQuestion> TableSourceViewModel;
            SurveySectionViewModel baseViewModel;
            public override nint RowsInSection(UITableView tableview, nint section) => ((TableSourceViewModel != null) ? TableSourceViewModel.Count : 0);
            public override nint NumberOfSections(UITableView tableView) => 1;

            public SectionQuestionTableSource(UITableView tableView, List<SurveyQuestion> viewModel, SurveySectionViewModel baseViewModel) : base(tableView)
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