using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco.StackTrace
{
    public class FileStackTraceProvider : IStackTraceProvider
    {
        private readonly string _filePath;

        public FileStackTraceProvider(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<string> ReadCurrentStackTraceLog()
        {
            if (File.Exists(_filePath))
            {
                var readCopy = _filePath + ".cpy";

                try
                {
                    File.Copy(_filePath, readCopy);

                    using (var reader = File.OpenText(readCopy))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
                finally
                {
                    File.Delete(readCopy);
                }
            }
            return null;
        }
    }
}
