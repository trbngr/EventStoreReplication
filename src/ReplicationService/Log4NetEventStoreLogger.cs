using System;
using EventStore.ClientAPI;
using log4net;

namespace ReplicationService
{
    public class Log4NetEventStoreLogger : ILogger
    {
        private static readonly ILog Log = LogManager.GetLogger("EventStore");

        public void Error(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        public void Error(Exception ex, string format, params object[] args)
        {
            Log.Error(ex);
            Log.ErrorFormat(format, args);
        }

        public void Info(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        public void Info(Exception ex, string format, params object[] args)
        {
            Log.Info(ex);
            Log.InfoFormat(format, args);
        }

        public void Debug(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        public void Debug(Exception ex, string format, params object[] args)
        {
            Log.Debug(ex);
            Log.DebugFormat(format, args);
        }
    }
}