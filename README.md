[![Downloads](https://img.shields.io/badge/download-nuget-blue.svg)](https://www.nuget.org/packages/archimedes.locco)

# Archimedes.Locco
Archimedes.Locco provides issue feedback and error consolidation for .NET Applications, supporting GitHub or simple e-mail as backend.


# Usage

```csharp
IIssueReportService _issueReportService = // Get hold of your IIssueReportService

// Show the error report dialog with a initial issue-report
var viewModel = new IssueReportDialogViewModel(_issueReportService, new IssueReport("App Crash Sample"));
var dlg = new IssueReportDialog(viewModel);
dlg.ShowDialog();
```


# Configuration
```csharp

IPropertyProvider configuration = new MemoryPropertyProvider(); // You would probably integrate your own configuration solution here
configuration.SetProperty("locco.github.appId", "github-app-id");
configuration.SetProperty("locco.github.token", "your very secret access token");
configuration.SetProperty("locco.github.owner", "YourRepositoryOwner");
configuration.SetProperty("locco.github.repository", "YourRepositoryName");

var issueReportService = new IssueReportService(configuration, new FileStackTraceProvider(AppUtil.AppDataFolder + @"\Logs\events.log"));

// The stacktrace provider reads stacktrace / error log informaiton.
// In the example above, it just includes the log4net log in the report.
```
