using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco.StackTrace
{
    public class MemoryStackTraceProvider : IStackTraceProvider
    {
        private readonly string _staticStackTrace;

        public MemoryStackTraceProvider(string staticStackTrace)
        {
            _staticStackTrace = staticStackTrace;
        }

        public async Task<string> ReadCurrentStackTraceLog()
        {
            return _staticStackTrace;
        }
    }
}
