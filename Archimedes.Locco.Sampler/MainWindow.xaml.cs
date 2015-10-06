using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Archimedes.Locco.UI;
using Archimedes.Locco.UI.Views;

namespace Archimedes.Locco.Sampler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IIssueReportService _issueReportService;


        public MainWindow(IIssueReportService issueReportService)
        {
            InitializeComponent();

            _issueReportService = issueReportService;
        }

        private async void BtnBasicClick(object sender, RoutedEventArgs args)
        {

            var issue = new IssueReport()
            {
                Title = "Sample Issue from locco-sampler",
                Description = "This issue is created automatically to test this library!",
                Stacktrace = @"2015-09-16 08:56:39,061 App.UI.MC.UF.SO.Load ERROR - System.Data.SqlClient.SqlException (0x80131904): The conversion of a varchar data type to a datetime data type resulted in an out-of-range value.
   bei System.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   bei System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   bei System.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   bei System.Data.SqlClient.SqlDataReader.TryHasMoreRows(Boolean& moreRows)
   bei System.Data.SqlClient.SqlDataReader.TryReadInternal(Boolean setTimeout, Boolean& more)
   bei System.Data.SqlClient.SqlDataReader.Read()
   bei System.Data.Common.DataAdapter.FillLoadDataRow(SchemaMapping mapping)
   bei System.Data.Common.DataAdapter.FillFromReader(DataSet dataset, DataTable datatable, String srcTable, DataReaderContainer dataReader, Int32 startRecord, Int32 maxRecords, DataColumn parentChapterColumn, Object parentChapterValue)
   bei System.Data.Common.DataAdapter.Fill(DataTable[] dataTables, IDataReader dataReader, Int32 startRecord, Int32 maxRecords)
   bei System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   bei System.Data.Common.DbDataAdapter.Fill(DataTable[] dataTables, Int32 startRecord, Int32 maxRecords, IDbCommand command, CommandBehavior behavior)
   bei System.Data.Common.DbDataAdapter.Fill(DataTable dataTable)
    ...
ClientConnectionId:26a388a2-c9cf-4e6e-bd6f-9462674493e2"
            };
            try
            {
                await _issueReportService.ReportIssueAsync(issue);

                MessageBox.Show("Issue has been successfully sent to backend!", "Success!");
            }
            catch (ReportSendException e)
            {
                MessageBox.Show("Failed to send issue to backend: " + ExceptionUtil.ToErrorMessage(e), "Issue send error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }



        private void BtnDialogClick(object sender, RoutedEventArgs e)
        {
            var viewModel = new IssueReportDialogViewModel(_issueReportService);
            var dlg = new IssueReportDialog(viewModel);
            dlg.ShowDialog();
        }
    }
}
