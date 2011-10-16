using System;
using System.Diagnostics;
using Caliburn.Micro;

namespace lsight
{
    public class DebugLog : ILog
    {
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(string.Format("INFO: " + format, args));
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(string.Format("WARN: " + format, args));
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine("ERROR: " + exception);
        }
    }
}