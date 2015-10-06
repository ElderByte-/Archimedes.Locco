using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Archimedes.Locco.Sampler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            var tokenPath = new FileInfo(ApplicaitonBinaryFolder + @"\Resources\token.txt");

            string token = null;
            if (tokenPath.Exists)
            {
                token = File.ReadAllText(tokenPath.FullName).Trim();
            }
            else
            {
                MessageBox.Show("Could not find token.txt with your private access token!", "No access token", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            var configuration = new MemoryPropertyProvider();
            configuration.SetProperty("locco.github.appId", "locco-sampler");
            configuration.SetProperty("locco.github.token", token);
            configuration.SetProperty("locco.github.owner", "ElderByte-");
            configuration.SetProperty("locco.github.repository", "Archimedes.Locco");


            var main = new MainWindow(configuration);
            main.Show();
        }


        /// <summary>
        /// Gets the applications binary folder, i.e. the folder where the exe resides.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown when the binary folder could not be retrived.</exception>
        private static string ApplicaitonBinaryFolder
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                if (assembly != null)
                {
                    return Path.GetDirectoryName(assembly.Location);
                }
                throw new NotSupportedException("Entry Assembly not available! This may occur in specail circumstances, such as when running as Unit Test.");
            }
        }
    }
}
