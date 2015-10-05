using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    /// <summary>
    /// Exception which is thrown when there was an issue sending an Issue Report
    /// </summary>
    public class ReportSendException : Exception
    {
        public ReportSendException(string message)
            :base(message)
        {
            
        }

        public ReportSendException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
