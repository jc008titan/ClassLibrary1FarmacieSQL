using FunctiiSQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ErrorLogs
    {
        static MySettings s = MySettings.Load();
        public void WriteLogError(string errorMessage)
        {
            try
            {
                string path = s.ErrorLogString + DateTime.Today.ToString("dd-mm-yy") + ".txt";
                if (!File.Exists(path))
                {
                    File.Create(path)
                   .Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void WriteLogException(Exception ex)
        {
            try
            {
                string path = s.ErrorLogString + DateTime.Today.ToString("dd-mm-yy") + ".txt";
                if (!File.Exists(path))
                {
                    File.Create(path)
                   .Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    var st = new StackTrace(ex, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(st.FrameCount - 1);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    var errpath = frame.GetFileName();
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error Message:" + ex.Message + " Error line:" + line.ToString() + " Error path:" + errpath.Replace("\\", "\\\\");
                    w.WriteLine(err);
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }

        }
    }
}
