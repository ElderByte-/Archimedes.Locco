using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Locco.BackendProviders;

namespace Archimedes.Locco
{
    public class IssueReportService
    {
        private readonly Dictionary<string, IReportBackend> _reportBackends = new Dictionary<string, IReportBackend>();

        #region Constructor

        /// <summary>
        /// Creates a new IssueReportService, without any report backends.
        /// You have to register them yourself using RegisterBackend()!
        /// </summary>
        public IssueReportService()
        {
            
        }

        /// <summary>
        /// Creates a new IssueReportService with the default report backends 
        /// </summary>
        /// <param name="configuration"></param>
        public IssueReportService(IPropertyProvider configuration)
        {
            if(configuration == null) throw new ArgumentNullException("configuration");
            RegisterBackend(new GitHubProvider(configuration));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/Sets the current backend id-name.
        /// </summary>
        public string ActiveBackend { get; set; }


        #endregion

        #region Public methods

        /// <summary>
        /// Registers a new report backend
        /// </summary>
        /// <param name="backend"></param>
        public void RegisterBackend(IReportBackend backend)
        {
            _reportBackends.Add(backend.IdName, backend);
            if (ActiveBackend == null)
            {
                ActiveBackend = backend.IdName;
            }
        }


        /// <summary>
        /// Reports the given issue to the current active backend
        /// </summary>
        /// <param name="report"></param>
        /// <exception cref="ReportSendException"></exception>
        public async Task ReportIssue(IssueReport report)
        {
            if(string.IsNullOrEmpty(ActiveBackend))throw new ReportSendException("You have to set an active report backend first before using the report method!");

            PrepareIssueReport(report);

            if (_reportBackends.ContainsKey(ActiveBackend))
            {
                var backend = _reportBackends[ActiveBackend];
                try
                {
                    await backend.SendIssueReportAsync(report);
                }
                catch (Exception e)
                {
                    throw new ReportSendException("Failed to send issue report using provider " + backend.GetType().Name, e);
                }
            }
            else
            {
                throw new ReportSendException(string.Format("No report backend found with id '{0}'", ActiveBackend));
            }
        }

        #endregion

        #region Protected methods

        protected virtual void PrepareIssueReport(IssueReport report)
        {
            if (report.Environment == null)
            {
                report.Environment = ResolveCurrentEnvironmentDetail();
            }

            if (report.Stacktrace == null)
            {
                report.Stacktrace = ResolveCurrentStacktrace();
            }
        }


        protected virtual EnvironmentDetail ResolveCurrentEnvironmentDetail()
        {
           return EnvironmentBuilder.Current();
        }

        protected virtual string ResolveCurrentStacktrace()
        {
            return "stack trace / log";
        }


        #endregion

    }
}
