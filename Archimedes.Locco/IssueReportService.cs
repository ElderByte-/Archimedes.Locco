﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Locco.BackendProviders;
using Archimedes.Locco.StackTrace;

namespace Archimedes.Locco
{
    public class IssueReportService : IIssueReportService
    {
        #region Fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private readonly Dictionary<string, IReportBackend> _reportBackends = new Dictionary<string, IReportBackend>();
        private readonly IStackTraceProvider _stackTraceProvider;
        private string _activeBackend;

        #endregion

        #region Constructors


        /// <summary>
        /// Creates a new IssueReportService with the default report backends (github and soon email)
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="stackTraceProvider"></param>
        public IssueReportService(IPropertyProvider configuration, IStackTraceProvider stackTraceProvider)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            _stackTraceProvider = stackTraceProvider;
            ActiveBackend = configuration.GetProperty("locco.backend");

            RegisterKnownProviders(configuration);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/Sets the current backend id-name.
        /// </summary>
        public string ActiveBackend
        {
            get { return _activeBackend; }
            set
            {
                if (_activeBackend != value)
                {
                    _activeBackend = value;
                    OnActiveBackendChanged(_activeBackend);
                }
            }
        }

        private void OnActiveBackendChanged(string newBackendId)
        {
            Log.Info(string.Format("IssueReport Backend has changed to '{0}'", newBackendId));
        }

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
        /// Reports the given issue to the current active backend.
        /// Certain properties such as the environment and stacktrace are being resolved automatically,
        /// if you leave them blank/null.
        /// 
        /// </summary>
        /// <param name="report">The issue report which will be sent to the backend.</param>
        /// <exception cref="ReportSendException">Thrown when there was a problem sending the report to the backend.</exception>
        public async Task ReportIssueAsync(IssueReport report)
        {
            if(string.IsNullOrEmpty(ActiveBackend))throw new ReportSendException("You have to set an active report backend first before using the report method!");

            await PrepareIssueReportAsync(report);

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

        private void RegisterKnownProviders(IPropertyProvider configuration)
        {
            RegisterBackend(new GitHubProvider(configuration));

        }

        protected async virtual Task PrepareIssueReportAsync(IssueReport report)
        {
            if (report.Environment == null)
            {
                report.Environment = ResolveCurrentEnvironmentDetail();
            }

            if (report.Stacktrace == null)
            {
                report.Stacktrace = await ResolveCurrentStacktraceAsync();
            }
        }


        protected virtual EnvironmentDetail ResolveCurrentEnvironmentDetail()
        {
           return EnvironmentBuilder.Current();
        }

        protected async virtual Task<string> ResolveCurrentStacktraceAsync()
        {
            try
            {
                return _stackTraceProvider != null ? await _stackTraceProvider.ReadCurrentStackTraceLog() : null;
            }
            catch (Exception e)
            {
                throw new ReportSendException("Failed to resolve current stacktrace!", e);
            }
        }


        #endregion

    }
}
