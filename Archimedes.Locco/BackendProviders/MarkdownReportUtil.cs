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

        public static string CompileBodyMarkdown(IssueReport report, int stackTraceMaxLenght)
        {

            var descriptionMd = CompileDescriptionMarkdown(report);
            var environmentMd = CompileEnvironmentMarkdown(report.Environment);
            var stacktraceMd = CompileStacktraceMarkdown(report.Stacktrace, stackTraceMaxLenght);

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

        private static string CompileStacktraceMarkdown(string stacktrace, int maxLenght)
        {
            if (string.IsNullOrEmpty(stacktrace)) return "Not available.";

            // Cut away from the stack trace / log if its too much data
            stacktrace = TrimStringLenght(stacktrace, maxLenght);

            return string.Format(
@"```
{0}
```", stacktrace);
        }

        public static string TrimStringLenght(string text, int maxLenght)
        {
            if (text.Length > maxLenght)
            {
                text = text.Substring(text.Length - maxLenght, maxLenght);
            }
            return text;
        }


        /// <summary>
        /// Quotes the given text in markdown syntax.
        /// Handles newlines and double newlines
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MarkdownQuote(string text)
        {
            if(text == null) throw new ArgumentNullException("text");

            // Incase we have double new-lines, we need to again add a quote start mark after each of them
            //var regex = new Regex("(\n\n|\r\n\r\n)");
            var regex = new Regex("((\n|\r\n){2})");

            var paragraphs = regex.Split(text);

            string quotetText = "";

            foreach (var paragraph in paragraphs)
            {
                if (!string.IsNullOrWhiteSpace(paragraph))
                {
                    var quotedParagraph = ">" + paragraph + Environment.NewLine + Environment.NewLine;
                    quotetText += quotedParagraph;
                }
            }

            return quotetText.Trim(Environment.NewLine.ToCharArray());
        }
    }
}
