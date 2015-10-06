using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    /// <summary>
    /// Provides the ability to send a issue report to a backend.
    /// </summary>
    public interface IIssueReportService
    {
        /// <summary>
        /// Registers a new report backend. 
        /// If no backend is currently active, this new one is set as active.
        /// </summary>
        /// <param name="backend"></param>
        void RegisterBackend(IReportBackend backend);

        /// <summary>
        /// Gets/Sets the current backend id-name which is used as target for the issue reports.
        /// </summary>
        string ActiveBackend { get; set; }


        /// <summary>
        /// Reports the given issue to the current active backend.
        /// Certain properties such as the environment and stacktrace are being resolved automatically,
        /// if you leave them blank/null.
        /// 
        /// </summary>
        /// <param name="report">The issue report which will be sent to the backend.</param>
        /// <exception cref="ReportSendException">Thrown when there was a problem sending the report to the backend.</exception>
        Task ReportIssueAsync(IssueReport report);
    }
}
