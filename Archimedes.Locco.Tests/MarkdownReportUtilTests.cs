using System;
using System.Collections.Generic;
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

        [Test]
        public void TestCompileBodyMarkdown()
        {
            IssueReport report = new IssueReport()
            {
                Title = "Unit Test Report",
                Description = "This is just a test from a unit test.",
                Stacktrace = "I am the stacktrace, me frend!",
                Environment = new EnvironmentDetail()
                {
                    AppName = "huhu"
                }
            };
            var markdown = MarkdownReportUtil.CompileBodyMarkdown(report, 1000);
        }

        [TestCase("Simple text", 100, "Simple text")]
        [TestCase("Simple text", 6, "e text")]
        public void TestCompileBodyMarkdown(string input, int maxLen, string expected)
        {
            Assert.AreEqual(expected, MarkdownReportUtil.TrimStringLenght(input, maxLen));
        }


    }
}
