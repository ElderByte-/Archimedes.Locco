using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;

namespace Archimedes.Locco.BackendProviders
{
    public class GitHubProvider : IReportBackend
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IPropertyProvider _propertyProvider;

        public GitHubProvider(IPropertyProvider propertyProvider)
        {
            _propertyProvider = propertyProvider;
        }

        public string IdName
        {
            get { return "github"; }
        }


        public async Task SendIssueReportAsync(IssueReport report)
        {
            var appId = _propertyProvider.GetProperty("locco.github.appId");
            var accessToken = _propertyProvider.GetProperty("locco.github.token");
            var repoOwner = _propertyProvider.GetProperty("locco.github.owner");
            var repoName = _propertyProvider.GetProperty("locco.github.repository");

            // Support default proxy
            var proxy = WebRequest.DefaultWebProxy;
            var httpClient = new HttpClientAdapter(() => HttpMessageHandlerFactory.CreateDefault(proxy));
            var connection = new Connection(new ProductHeaderValue(appId), httpClient);

            if (proxy != null)
            {
                Log.Info(string.Format("Using proxy server '{0}' to connect to github!", proxy.GetProxy(new Uri("https://api.github.com/"))));
            }


            var client = new GitHubClient(connection)
            {
                Credentials = new Credentials(accessToken)
            };


            // Creates a new GitHub Issue
            var newIssue = new NewIssue(report.Title)
            {
                Body = MarkdownReportUtil.CompileBodyMarkdown(report, 220000),
            };
            newIssue.Labels.Add("bot");

            var issue = await client.Issue.Create(repoOwner, repoName, newIssue);
        }


    }
}
