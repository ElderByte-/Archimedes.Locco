namespace Archimedes.Locco.UI.Views
{
    /// <summary>
    /// Interaction logic for IssueReportDialog.xaml
    /// </summary>
    public partial class IssueReportDialog
    {
        public IssueReportDialog(IssueReportDialogViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
