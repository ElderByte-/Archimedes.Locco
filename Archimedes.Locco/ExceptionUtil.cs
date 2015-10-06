using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    public static class ExceptionUtil
    {
        public static string ToErrorMessage(Exception e)
        {
            string message = "";
            while (e != null)
            {
                message += e.Message + " ";
                e = e.InnerException;
            }
            return message;
        }
    }
}
