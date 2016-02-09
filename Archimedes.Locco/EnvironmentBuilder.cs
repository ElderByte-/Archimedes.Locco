using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    public class EnvironmentBuilder
    {
        public static EnvironmentDetail Current()
        {
            var environment = new EnvironmentDetail();

            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                environment.AppName = versionInfo.ProductName;
            }
            environment.AppVersion = Version;
            environment.User = Environment.UserName;
            environment.ComputerName = Environment.MachineName;
            environment.OperatingSystem = Environment.OSVersion.ToString();
            environment.CurrentProcessArchitecture = (IntPtr.Size * 8) + "bit";
            environment.DotNetVersion = Environment.Version.ToString();

            return environment;
        }


        private static string Version
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                else
                {
                    var entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        return entryAssembly.GetName().Version.ToString();
                    }
                    return "unknown";
                }
            }
        }
    }
}
