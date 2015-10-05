using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Locco
{
    public interface IPropertyProvider
    {
        string GetProperty(string key);
    }

    public class MemoryPropertyProvider : IPropertyProvider
    {
        readonly IDictionary<string, string> _properties = new Dictionary<string, string>();


        public void SetProperty(string key, string value)
        {
            if (_properties.ContainsKey(key))
            {
                _properties[key] = value;
            }
            else
            {
                _properties.Add(key, value);
            }
        }

        public string GetProperty(string key)
        {
            if (_properties.ContainsKey(key))
            {
                return _properties[key];
            }
            return null;
        }
    }
}
