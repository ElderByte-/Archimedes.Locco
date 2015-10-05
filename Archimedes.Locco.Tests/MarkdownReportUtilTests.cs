using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Locco.BackendProviders;
using NUnit.Framework;

namespace Archimedes.Locco.Tests
{
    [TestFixture]
    public class MarkdownReportUtilTests
    {
        [Test]
        public void TestMarkdownQuoteSimpleLine()
        {
            Assert.AreEqual(">Im a simple line of text", MarkdownReportUtil.MarkdownQuote("Im a simple line of text"));  ;
        }


        [Test]
        public void TestMarkdownQuoteNewlineSingle()
        {
            Assert.AreEqual(
@">Im a simple line of text!
And I continue on next line?", MarkdownReportUtil.MarkdownQuote(
@"Im a simple line of text!
And I continue on next line?")); ;
        }

        [Test]
        public void TestMarkdownQuoteNewlineMulti()
        {
            Assert.AreEqual(
@">Im a simple line of text!

>And I continue on over next line?", MarkdownReportUtil.MarkdownQuote(
@"Im a simple line of text!

And I continue on over next line?")); ;
        }

        public void TestCompileBodyMarkdown()
        {
            IssueReport report = new IssueReport()
            {
                Title = "Unit Test Report",
                Description = "This is just a test from a unit test.",
                Stacktrace = "I am the stacktrace, me frend!",
            };

            report.Environment.AppName = "";

            var markdown = MarkdownReportUtil.CompileBodyMarkdown(report);
        }

    }
}
