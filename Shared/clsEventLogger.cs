using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class clsEventLogger
    {
        static string sourceName = "DVLD";
        public static void LogEvent(Exception ex, string Location, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
            string message =
                   $"----------------------------------\n" +
                   $"Message        : {ex.Message}\n" +
                   $"Exception Date : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n" +
                   $"Location :        {Location}\n" +
                   $"StackTrace:\n{ex.StackTrace}\n" +
                   $"----------------------------------";

            EventLog.WriteEntry(sourceName, message, type);
        }
    }
}
