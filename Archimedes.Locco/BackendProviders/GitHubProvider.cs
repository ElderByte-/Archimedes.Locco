using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;

namespace Archimedes.Locco.BackendProviders
{
    public class GitHubProvider : IReportBackend
    {
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


            var client = new GitHubClient(new ProductHeaderValue(appId))
            {
                Credentials = new Credentials(accessToken)
            };

            // Creates a new GitHub Issue
            var newIssue = new NewIssue(report.Title)
            {
                Body = MarkdownReportUtil.CompileBodyMarkdown(report, 220000),
            };
            //newIssue.Labels.Add("locco");

            var issue = await client.Issue.Create(repoOwner, repoName, newIssue);
        }


    }
}
