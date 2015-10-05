using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archimedes.Locco.BackendProviders
{
    public static class MarkdownReportUtil
    {

        public static string CompileBodyMarkdown(IssueReport report)
        {

            var descriptionMd = CompileDescriptionMarkdown(report);
            var environmentMd = CompileEnvironmentMarkdown(report.Environment);
            var stacktraceMd = CompileStacktraceMarkdown(report.Stacktrace);

            var markdown = string.Format(
@"
### Description
{0}

### Environment
{1}


### Stacktrace
{2}
", descriptionMd, environmentMd, stacktraceMd);

            return markdown;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        private static string CompileDescriptionMarkdown(IssueReport report)
        {
            return string.Format(
@"
**{0}** _reports:_
{1}
", report.Environment.User, MarkdownQuote(report.Description));
        }





        private static string CompileEnvironmentMarkdown(EnvironmentDetail environment)
        {
            // Lets create a nice markdown table
            return string.Format(
@"
Property | Value
------------ | -------------
Application | {0}
Version  | {1}
User  | {2}
Computer Name  |  {3}
Operating System  | {4}
.NET Version  | {5}
Process Architecture | {6}
", environment.AppName, environment.AppVersion, environment.User, environment.ComputerName,
environment.OperatingSystem, environment.DotNetVersion, environment.CurrentProcessArchitecture);
        }

        private static string CompileStacktraceMarkdown(string stacktrace)
        {
            if (string.IsNullOrEmpty(stacktrace)) return "Not available.";

            return string.Format(
@"```
{0}
```", stacktrace);
        }


        /// <summary>
        /// Quotes the given text in markdown syntax.
        /// Handles newlines and double newlines
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MarkdownQuote(string text)
        {
            var quotetText = ">" + text;

            // Incase we have double new-lines, we need to again add a quote start mark after each of them
            var regex = new Regex("(\n\n|\r\n\r\n)");
            quotetText = regex.Replace(quotetText, "$1>");

            return quotetText;
        }
    }
}
