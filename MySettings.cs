using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctiiSQL
{
    class MySettings : AppSettings<MySettings>
    {
        public string ConnectionString="";
        public string ErrorLogString = "";
    }
}
