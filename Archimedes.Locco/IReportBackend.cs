using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    /// <summary>
    /// Represents a backend for issue reports
    /// </summary>
    public interface IReportBackend
    {
        /// <summary>
        /// Send the given report to the backend 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        Task SendIssueReportAsync(IssueReport report);

        /// <summary>
        /// Gets the id name of this backend provider
        /// </summary>
        string IdName { get;}
    }
}
