using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FunctiiSQL
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "AppSettings.json";

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
            {
                var text = File.ReadAllText(fileName);
                var json = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
                t = JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
            }
                return t;
        }
    }
}
