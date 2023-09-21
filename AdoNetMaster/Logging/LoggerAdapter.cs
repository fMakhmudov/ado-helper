using AdoNetMaster.Core.Interfaces;
using AdoNetMaster.Models;
using System;
using System.Runtime.CompilerServices;

namespace AdoNetMaster.WebApi.Logging
{
    public class LoggerAdapter : IAppLogger
    {
        //Apini teskshirish uchun
        public void Trace(string message,
            string fileSuffix = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => Log.Trace(message, fileSuffix, callerFilePath, callerLineNumber, callerMember);

        //vaqrinchalik test davomida yoziladigan loglar uchun
        public void Debug(string message,
           [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => Log.Debug(message, callerFilePath, callerLineNumber, callerMember);

        //dasturdagi muhum ma'lumotlarni bilish uchun
        public void Info(string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => Log.Info(message, callerFilePath, callerLineNumber, callerMember);

        //extimoliy errorlarni yozish uchun
        public void Error(string message,
           [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => Log.Error(message, callerFilePath, callerLineNumber, callerMember);

        //catch ga tushga errorlar uchun
        public void Critical(string message,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerMember = ""
        ) => Log.Critical(message, callerFilePath, callerLineNumber, callerMember);

        //catch ga tushga erorlar uchun
        public void Critical(Exception ex) => Log.Critical(ex);


        public void DbError(string message,
        [CallerFilePath] string callerFilePath = "",
         [CallerLineNumber] int callerLineNumber = 0,
         [CallerMemberName] string callerMember = ""
       ) => Log.DbError(message, callerFilePath, callerLineNumber, callerMember);

        public void DbCritical(string message,
           [CallerFilePath] string callerFilePath = "",
           [CallerLineNumber] int callerLineNumber = 0,
           [CallerMemberName] string callerMember = ""
       ) => Log.DbCritical(message, callerFilePath, callerLineNumber, callerMember);

        public void DbCritical(Exception ex) => Log.DbCritical(ex);
    }
}
