using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco.StackTrace
{
    public class Log4NetStackTraceProvider : IStackTraceProvider
    {
        private readonly string _logsFolder;

        public Log4NetStackTraceProvider(string logsFolder)
        {
            _logsFolder = logsFolder;
        }

        public async Task<string> ReadCurrentStackTraceLog()
        {
            throw new NotImplementedException();
        }
    }
}
