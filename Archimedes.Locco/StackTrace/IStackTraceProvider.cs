using System.Threading.Tasks;

namespace Archimedes.Locco.StackTrace
{
    public interface IStackTraceProvider
    {
        /// <summary>
        /// Read the current stacktrace and log into a simple string.
        /// This will be included in the error report for analysis.
        /// </summary>
        /// <returns></returns>
        Task<string> ReadCurrentStackTraceLog();
    }
}
