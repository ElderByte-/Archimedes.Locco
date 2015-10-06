using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Archimedes.Localisation;
using Archimedes.Locco.UI.Utils;
using Octokit;

namespace Archimedes.Locco.UI
{
    public class IssueReportDialogViewModel : ViewModelBase
    {
        private readonly IIssueReportService _issueReportService;
        private Visibility _progressVisibility = Visibility.Collapsed;

        private readonly IssueReportViewModel _issueReportViewModel;

        public IssueReportDialogViewModel(IIssueReportService issueReportService, IssueReport report)
        {
            _issueReportService = issueReportService;
            _issueReportViewModel = new IssueReportViewModel(report);
        }


        public IssueReportViewModel IssueReportViewModel {
            get { return _issueReportViewModel; }
        }


        public Visibility ProgressVisibility
        {
            get
            {
                return _progressVisibility;
            }
            set
            {
                _progressVisibility = value;
                OnPropertyChanged("ProgressVisibility");
            }
        }

        public ICommand SendCommand
        {
            get
            {
                return new RelayCommand<Window>(async x =>
                {
                    ProgressVisibility = Visibility.Visible;

                    try
                    {
                        // Send the report
                        await _issueReportService.ReportIssueAsync(IssueReportViewModel.Issue);

                        // Close the window after success
                        CloseCommand.Execute(x);
                    }
                    catch (ReportSendException e)
                    {
                        MessageBox.Show(Translator.GetTranslation("locco.report.send.failed.description") + "\n\n"+ ExceptionUtil.ToErrorMessage(e),
                            Translator.GetTranslation("locco.report.send.failed.title"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        ProgressVisibility = Visibility.Collapsed;
                    }
                }, x =>
                {
                    var issue = IssueReportViewModel.Issue;
                    return !string.IsNullOrEmpty(issue.Title) && !string.IsNullOrEmpty(issue.Description);
                });
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand<Window>(x =>
                {
                    x.Close();
                });
            }
        }

    }
}
