using System;
using System.IO; 
using System.Runtime.CompilerServices;

namespace AdoNetMaster.Models
{
    public static class Log
    {
        private static readonly long maxfilesize = 3 * 1024 * 1024;
        private static readonly string logFileName = "";
        private static void LogFormat(string level, string callerMember, string callerFilePath, int callerLineNumber, string msg, string filename)
        {
            string message = string.Format("Time: {0}  Level: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"), level);
            message += Environment.NewLine;
            message += "--------------------------------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Caller File Path: {0}", callerFilePath);
            message += Environment.NewLine;
            message += string.Format("Caller Member: {0}", callerMember);
            message += Environment.NewLine;
            message += string.Format("Caller Line Number: {0}", callerLineNumber);
            message += Environment.NewLine;
            message += string.Format("Message: {0}", msg);
            message += Environment.NewLine;
            message += "********************************************************************************";
            message += Environment.NewLine;

            LogWrite(message, filename);
        }

        private static void LogFormat(Exception ex, string filename)
        {
            if(ex != null)
            {
                if (ex is AggregateException ae)
                {
                    foreach (var ie in ae.Flatten().InnerExceptions)
                    {
                       if(ie != null)
                        {
                            string message = string.Format("Time: {0}  Level: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"), "[CRITICAL]");
                            message += Environment.NewLine;
                            message += "--------------------------------------------------------------------------------";
                            message += Environment.NewLine;
                            message += string.Format("Source: {0}", ie.Source);
                            message += Environment.NewLine;
                            message += string.Format("StackTrace: {0}", ie.StackTrace);
                            message += Environment.NewLine;
                            message += string.Format("TargetSite: {0}", ie.TargetSite?.ToString());
                            message += Environment.NewLine;
                            message += string.Format("Message: {0}", ie.Message);
                            message += Environment.NewLine;
                            message += "********************************************************************************";
                            message += Environment.NewLine;

                            LogWrite(message, filename);
                        }
                    }
                }
                else
                {
                    string message = string.Format("Time: {0}  Level: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt"), "[CRITICAL]");
                    message += Environment.NewLine;
                    message += "--------------------------------------------------------------------------------";
                    message += Environment.NewLine;
                    message += string.Format("Source: {0}", ex.Source);
                    message += Environment.NewLine;
                    message += string.Format("StackTrace: {0}", ex.StackTrace);
                    message += Environment.NewLine;
                    message += string.Format("TargetSite: {0}", ex.TargetSite?.ToString());
                    message += Environment.NewLine;
                    message += string.Format("Message: {0}", ex.Message);
                    message += Environment.NewLine;
                    message += "********************************************************************************";
                    message += Environment.NewLine;

                    LogWrite(message, filename);
                }
            }
        }

        private static void LogWrite(string message, string filename)
        {
            try
            {
                string file_name = logFileName[0..^4] + "_" + filename + ".txt";
                if (File.Exists(file_name))
                {
                    long length = new FileInfo(file_name).Length;
                    if (length > maxfilesize)
                    {
                        string logdest = file_name[0..^4] + "_" + DateTime.Now.Ticks + ".txt";
                        File.Move(file_name, logdest);
                    }
                }
                else
                {
                    var fi = new FileInfo(file_name);
                    fi.Directory.Create();
                }
                File.AppendAllText(file_name, message);
            }
            catch { }
        }

        //Apini teskshirish uchun
        public static void Trace(string message,
            string fileSuffix = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => LogFormat("[TRACE]", callerMember, callerFilePath, callerLineNumber, message, "trace_" + fileSuffix);

        //vaqrinchalik test davomida yoziladigan loglar uchun
        public static void Debug(string message,
           [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => LogFormat("[DEBUG]", callerMember, callerFilePath, callerLineNumber, message, "debug");

        //dasturdagi muhum ma'lumotlarni bilish uchun
        public static void Info(string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => LogFormat("[INFO]", callerMember, callerFilePath, callerLineNumber, message, "info");

        //extimoliy errorlarni yozish uchun
        public static void Error(string message,
           [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => LogFormat("[ERROR]", callerMember, callerFilePath, callerLineNumber, message, "error");

        //catch ga tushga errorlar uchun
        public static void Critical(string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => LogFormat("[CRITICAL]", callerMember, callerFilePath, callerLineNumber, message, "error");

        //catch ga tushga erorlar uchun
        public static void Critical(Exception ex) => LogFormat(ex, "error");


        public static void DbError(string message,
        [CallerFilePath] string callerFilePath = "",
         [CallerLineNumber] int callerLineNumber = 0,
         [CallerMemberName] string callerMember = ""
       ) => LogFormat("[DB ERROR]", callerMember, callerFilePath, callerLineNumber, message, "error");

        public static void DbCritical(string message,
           [CallerFilePath] string callerFilePath = "",
           [CallerLineNumber] int callerLineNumber = 0,
           [CallerMemberName] string callerMember = ""
       ) => LogFormat("[DB CRITICAL]", callerMember, callerFilePath, callerLineNumber, message, "error");

        public static void DbCritical(Exception ex) => LogFormat(ex, "error");
    }
}