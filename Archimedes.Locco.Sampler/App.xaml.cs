using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Archimedes.Localisation.Utils;
using Archimedes.Locco.StackTrace;
using log4net.Core;

namespace Archimedes.Locco.Sampler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            new LoggerConfiguration(AppUtil.AppDataFolder, Level.All);

            Log.Info("Starting up Archimedes.Locco.Sampler...");

            var configuration = new MemoryPropertyProvider();
            configuration.SetProperty("locco.github.appId", "locco-sampler");
            configuration.SetProperty("locco.github.token", ReadAccessToken());
            configuration.SetProperty("locco.github.owner", "ElderByte-");
            configuration.SetProperty("locco.github.repository", "Archimedes.Locco");

            var issueReportService = new IssueReportService(configuration, new FileStackTraceProvider(AppUtil.AppDataFolder + @"\Logs\events.log"));

            var main = new MainWindow(issueReportService);
            main.Show();
        }




        private string ReadAccessToken()
        {
            var tokenPath = new FileInfo(AppUtil.ApplicaitonBinaryFolder + @"\Resources\token.txt");

            string token = null;
            if (tokenPath.Exists)
            {
                token = File.ReadAllText(tokenPath.FullName).Trim();
            }
            else
            {
                var errorMsg = "Could not find token.txt with your private access token!";
                Log.Warn(errorMsg);
                MessageBox.Show(errorMsg, "No access token", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return token;
        }

    }
}
