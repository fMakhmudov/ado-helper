using System;
using System.Runtime.CompilerServices;

namespace AdoNetMaster.Core.Interfaces
{
    public interface IAppLogger
    {
        public void Critical(Exception ex);
        public void Critical(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
        public void DbCritical(Exception ex);
        public void DbCritical(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
        public void DbError(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
        public void Debug(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
        public void Error(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
        public void Info(string message, [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0, [CallerMemberName] string callerMember = "");
    }
}
