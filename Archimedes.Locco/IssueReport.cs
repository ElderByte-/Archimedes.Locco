using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    public class EnvironmentDetail
    {

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string User { get; set; }

        public string ComputerName { get; set; }

        public string OperatingSystem { get; set; }

        public string DotNetVersion { get; set; }
        public string CurrentProcessArchitecture { get; set; }
    }

    /// <summary>
    /// Represents an issue report which can be sent to a supported backend service.
    /// </summary>
    public class IssueReport
    {

        public IssueReport()
        {
        }

        /// <summary>
        /// The title of the issue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Holds the description of the issue.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds detailed stacktrace or log information, if available
        /// </summary>
        public string Stacktrace { get; set; }

        /// <summary>
        /// Holds environment information of the current process, user and system
        /// </summary>
        public EnvironmentDetail Environment { get; set; }
    }
}
